using LandmassTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.ChessStuff
{
    public class Knight : Piece
    {
        public Knight(ChessCell startingCell, bool isLocalTeam) : base(startingCell, isLocalTeam)
        {
        }

        public override List<ChessCell> GetUnrestrictedAttackedCells(List<Piece> allPieces)
        {
            var observed = GetObservedDirectionsAndCells();

            var valid = observed.SelectMany(x => x.Cells)
                .ToList()
                .FilterFriendlyPieces(this)
                .ToList();
            return valid;
        }

        public override List<(Direction direction, List<ChessCell> Cells)> GetObservedDirectionsAndCells()
        {
            List<(Direction Direction, List<ChessCell> Cells)> sex = new();

            int[] dx = { -1, 1, -1, -2, -2, -1, 1, 2 };
            int[] dy = { 2, 2, 2, 1, -1, -2, -2, -1 };

            foreach (var directionIndex in Enumerable.Range(0, dx.Length))
            {
                int nextX = dx[directionIndex];
                int nextY = dy[directionIndex];
                Direction horizontal = DirectionHelper.IntToHorizontal(nextX);
                Direction vertical = DirectionHelper.IntToVertical(nextY);

                var cellPosition = this.Cell.GetCellRelativePositionOrDefault
                    (horizontal, Math.Abs(nextX),
                    vertical, Math.Abs(nextY));

                if (cellPosition is not null)
                {
                    sex.Add((Direction.South, cellPosition.AsList()));
                }
            }

            return sex;
        }
        public override string ToString()
        {
            return "C";
        }
    }
}
