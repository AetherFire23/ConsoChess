using LandmassTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.ChessStuff
{
    public class Bishop : Piece
    {
        public Bishop(ChessCell startingCell, bool isLocalTeam) : base(startingCell, isLocalTeam)
        {
        }

        public override List<(Direction direction, List<ChessCell> Cells)> GetObservedDirectionsAndCells()
        {
            var observedTargets = DirectionHelper
                .GetDiagonalDirections()
                .Select(x => (x, Cell.GetCellsInDirectionLine(x)))
                .ToList();

            return observedTargets;
        }

        public override List<ChessCell> GetValidCellMoves(List<Piece> allPieces)
        {
            var observedTargets = GetObservedDirectionsAndCells();
            var valid = observedTargets
                .Select(x => x.Cells.GetCellsUntil(this, true, true))
                .SelectMany(x => x).ToList();

            return base.RestrictPieceMovementDueToPinsOrKingCheck(allPieces, valid);
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
