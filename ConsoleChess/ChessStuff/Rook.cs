﻿using LandmassTests;

namespace ConsoleChess.ChessStuff
{
    public class Rook : Piece
    {
        public Rook(ChessCell startingCell, bool isLocalTeam) : base(startingCell, isLocalTeam)
        {
        }

        public override List<(Direction direction, List<ChessCell> Cells)> GetObservedDirectionsAndCells()
        {
            var horizontalAndVertical = Cell.GetHorizontalAndVerticalCellLines();
            return horizontalAndVertical;
        }

        public override List<ChessCell> GetValidCellMoves(List<Piece> allPieces)
        {
            var horizontalAndvertical = GetObservedDirectionsAndCells();
            var withoutFirstHit = horizontalAndvertical
                .Select(x => x.Cells.GetCellsUntil(this, true, false))
                .SelectMany(x => x)
                .ToList();
            return RestrictPieceMovementDueToPinsOrKingCheck(allPieces, withoutFirstHit);
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
