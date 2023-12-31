﻿using ConsoleChess.GridBase;
using LandmassTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.ChessStuff
{
    public abstract class Piece
    {
        public ChessCell Cell { get; set; }
        // This should in fact really be updated higher up becuase
        // knights and pawns could 
        // used to verify if the king can eat on a piece that is protected
        // since it cant put itself in check
        public List<ChessCell> ValidCellsIncludingFriendlyAndFirstBlockingPiece { get; set; } = new();

        public bool IsCheckingEKing => IsCheckingEnemyKing();
        public bool IsObservingEnemyKing => GetLineOfPiecesObservingOppositeKing().Any();

        public bool IsLocalTeam;
        public ConsoleColor Color => IsLocalTeam
            ? ConsoleColor.Cyan
            : ConsoleColor.Yellow;
        public Piece(ChessCell startingCell, bool isLocalTeam)
        {
            Cell = startingCell;
            IsLocalTeam = isLocalTeam;
            Cell.Piece = this;
        }

        public virtual void MoveToOtherCell(ChessCell nextCell)
        {
            // big problems when eating pieces
            if (nextCell.HasPiece)
            {
                //Cell.Piece.Cell = null; // no cross=refrences

            }

            Cell.Piece = null;
            nextCell.Piece = this;
            this.Cell = nextCell;
        }

        public abstract List<(Direction direction, List<ChessCell> Cells)> GetObservedDirectionsAndCells();
        public abstract List<ChessCell> GetUnrestrictedAttackedCells(List<Piece> allPieces);

        public List<ChessCell> GetValidCellMoves(List<Piece> allPieces)
        {
            var unrestrictedAttacked = GetUnrestrictedAttackedCells(allPieces);
            var restricted = RestrictPieceMovementDueToPinsOrKingCheck(allPieces, unrestrictedAttacked);
            return restricted;
        }

        public List<ChessCell> RestrictPieceMovementDueToPinsOrKingCheck(List<Piece> allPieces, List<ChessCell> otherwiseValidMoves)
        {
            var teamInfo = new MoveInfo(allPieces, this);

            // restrict moves due to This piece being pinned to the king
            if (this.IsPinned(teamInfo.EnemyPieces, teamInfo.FriendlyKing)) return new List<ChessCell>();

            // restrict moves due to king being checked
            // a move cannot be allowed to unpin a king if there are 2 attacking pieces. cant block attacks from both sides in 1 move!
            var checkingPieces = teamInfo.EnemyPieces.Where(x => x.IsCheckingEKing).ToList();
            if (!checkingPieces.Any()) return otherwiseValidMoves; // king not in check

            if (this is not King)
            {
                if (checkingPieces.Count > 1) return new List<ChessCell>(); // cannot protect the king if 2 piecs attack at the same time

                var attackingPiece = checkingPieces.First();
                // here add some conditions that spcifies that if you can capture uncheck the king  if you can eat the attacker

                // check if it can eat the attackingPiece
                var remainingMovesLeft = otherwiseValidMoves
                .Where(cell =>
                MoveUnchecksFriendlyKingByIntersectingAttackLine(teamInfo.FriendlyKing, checkingPieces.First(), cell)
                || MoveEatsPieceIfUnrestricted(allPieces, attackingPiece))
                .ToList();

                return remainingMovesLeft;
            }

            // the only allowd moves for the king is the one where no valid squares where the enemy pieces can go.
            // There is furtehr restriction i think directly in the king class so I think i can delete this
            var validAttackingPieceMoves = checkingPieces.Select(x => x.GetValidCellMoves(allPieces)).SelectMany(x => x).ToList();
            var validMoves = otherwiseValidMoves.Where(x => !validAttackingPieceMoves.Contains(x)).ToList();
            return validMoves;
        }

        public bool IsCheckingEnemyKing()
        {
            var lineWithOppositeKing = GetLineOfPiecesObservingOppositeKing();
            if (!lineWithOppositeKing.Any()) return false;

            var enemyKing = lineWithOppositeKing
                .First(p => p is King
                 && this.IsOppositeTeamPiece(p));

            // a piece has a direct line of sight on the opposite king
            bool isKingFirst = lineWithOppositeKing.IndexOf(enemyKing) == 0;

            return isKingFirst;
        }

        public bool IsPinned(List<Piece> enemyPieces, King friendlyKing) // works
        {
            var linesOfPiecesFacingFriendlyKing = enemyPieces
                .Where(x => x.IsObservingEnemyKing)
                .Where(x => x.GetLineOfPiecesObservingOppositeKing().Contains(this))
                .Select(x => x.GetLineOfPiecesObservingOppositeKing())
                .Where(x => x.Count < 2).ToList(); // important : if more than 1 piece in face of king in line, it means that no piece can be pinned

            bool isPinned = linesOfPiecesFacingFriendlyKing
                .Any(x => x.IndexOf(this) < x.IndexOf(friendlyKing));

            return isPinned;
        }

        // since its always gonna be no for all pieces, push the attackingpieces.count < 1 higher
        public bool MoveUnchecksFriendlyKingByIntersectingAttackLine(King friendlyKing, Piece attackingPiece, ChessCell cellMovePos)
        {
            List<ChessCell> lineAttackingKing = attackingPiece.GetLineOfCellsObservingOppositeKing();
            if (lineAttackingKing.Count == 1) return false;// no one can get in between 1 line of 1 length

            var moveDistance = lineAttackingKing.FirstOrDefault(x => x.Position.Equals(cellMovePos.Position));
            if (moveDistance is null) return false; // move is not found 

            int indexOfMovePosition = lineAttackingKing.IndexOf(cellMovePos);
            int indexOfKing = lineAttackingKing.IndexOf(friendlyKing.Cell);

            // if the moving piece is closer than the king, it obfuscates the view.
            // therefore the king is unpinned.
            if (indexOfMovePosition < indexOfKing) return true;

            return false;
        }

        public List<List<ChessCell>> GetObservedCellsForEachDirection()
        {
            var observedCells = GetObservedDirectionsAndCells();
            if (!observedCells.Any()) return new List<List<ChessCell>>();

            var cellsInDirection = GetObservedDirectionsAndCells()
                .Select(x => x.Item2).ToList();

            return cellsInDirection;
        }
        public List<List<Piece>> GetObservedPiecesForEachDirection()
        {
            var observedCels = GetObservedCellsForEachDirection();
            if (!observedCels.Any()) return new List<List<Piece>>();

            var linesWithPieces = observedCels.Where(x => x.Any(x => x.Piece is not null)).ToList();
            var pieces = linesWithPieces
                .Select(x => x.Where(x => x.Piece is not null)
                .Select(x => x.Piece).ToList()).ToList();

            //.Where(z => !z.Any(p => p.Piece is null))
            //.Select(x => x.Select(y => y.Piece).ToList()).ToList() ?? new List<List<Piece>>();
            return pieces;
        }

        public List<Piece> GetLineOfPiecesObservingOppositeKing()
        {
            var observedPieces = GetObservedPiecesForEachDirection();
            if (!observedPieces.Any()) return new List<Piece>();

            var line = observedPieces
                .Where(line =>
                    line.Any(piece => piece is King
                    && this.IsOppositeTeamPiece(piece))).ToList();

            var notNull = line.Any() ? line.First() : new List<Piece>();

            return notNull;
        }

        public List<ChessCell> GetLineOfCellsObservingOppositeKing()
        {
            var cells = GetObservedCellsForEachDirection()
                .Where(x => x.Any(c => c.Piece is King && IsOppositeTeamPiece(c.Piece))).ToList();

            var notNull = cells.Any()
                ? cells.First()
                : new List<ChessCell>();

            return notNull;
        }

        public bool IsOppositeTeamPiece(Piece piece)
        {
            bool isOpposite = this.IsLocalTeam != piece.IsLocalTeam;
            return isOpposite;
        }

        public bool MoveEatsPieceIfUnrestricted(List<Piece> allPieces, Piece piece)
        {
            var attackedCells = GetUnrestrictedAttackedCells(allPieces);
            bool canEat = attackedCells.Contains(piece.Cell);
            return canEat;
        }
    }
}
