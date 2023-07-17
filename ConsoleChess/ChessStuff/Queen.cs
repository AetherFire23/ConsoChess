using LandmassTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.ChessStuff
{
    public class Queen : Piece
    {
        public Queen(ChessCell startingCell, bool isLocal) : base(startingCell, isLocal)
        {
        }

        public override List<ChessCell> GetUnrestrictedAttackedCells(List<Piece> allPieces)
        {
            List<ChessCell> valid2 = GetObservedDirectionsAndCells()
                .Select(x => x.Cells.GetCellsUntil(this, true, true))
                .SelectMany(x => x).ToList();
            return valid2;
        }

        public override List<(Direction direction, List<ChessCell> Cells)> GetObservedDirectionsAndCells()
        {
            var allDirectionLines = Cell.GetDiagonalCellLines()
                .Union(Cell.GetHorizontalAndVerticalCellLines()).ToList();
            return allDirectionLines;
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}
