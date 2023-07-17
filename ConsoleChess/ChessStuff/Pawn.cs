using LandmassTests;

namespace ConsoleChess.ChessStuff
{
    public class Pawn : Piece
    {

        public bool IsFirstMove = true;
        public Pawn(ChessCell startingCell, bool isLocalTeam) : base(startingCell, isLocalTeam)
        {

        }

        public override List<(Direction direction, List<ChessCell> Cells)> GetObservedDirectionsAndCells()
        {
            Direction leftDirection = IsLocalTeam
                ? Direction.NorthWest
                : Direction.SouthWest;

            Direction rightDirection = IsLocalTeam
                ? Direction.NorthEast
                : Direction.SouthEast;
            ChessCell? leftCaptureCell = Cell.GetCellForDistanceOrDefault(leftDirection, 1);
            ChessCell? rightCaptureCell = Cell.GetCellForDistanceOrDefault(rightDirection, 1);

            var observedCells = new List<(Direction direction, List<ChessCell> Cells)>();
            if (leftCaptureCell is not null)
            {
                observedCells.Add((leftDirection, leftCaptureCell.AsList()));
            }
            if (rightCaptureCell is not null)
            {
                observedCells.Add((rightDirection, rightCaptureCell.AsList()));

            }
            return observedCells;
        }

        private List<ChessCell> GetForwardMoveCells(Direction forwardDirection)
        {
            int maximumForwardDistance = IsFirstMove ? 2 : 1;
            List<ChessCell> forwardCells = Cell.GetCellsInDirectionLineAndDistance(forwardDirection, maximumForwardDistance);

            var firstPieceFound = forwardCells.FirstOrDefault(x => x.HasPiece);
            if (forwardCells.Any() && firstPieceFound is not null)
            {
                int index = forwardCells.IndexOf(firstPieceFound);
                forwardCells = forwardCells.Take(index).ToList();

            }
            return forwardCells;
        }
        public override List<ChessCell> GetValidCellMoves(List<Piece> allPieces)
        {
            var validCells = new List<ChessCell>();
            foreach ((Direction direction, List<ChessCell> Cells) in GetObservedDirectionsAndCells())
            {
                // if piece is null or is opposite team
                Piece? piece = Cells.First().Piece;
                if (piece is null || !IsOppositeTeamPiece(piece)) continue;

                validCells.Add(piece.Cell);
            }
            // i need to add the forward moves since they are not observed by default.
            Direction forwardDirection = IsLocalTeam
                ? Direction.North
                : Direction.South;
            validCells.AddRange(GetForwardMoveCells(forwardDirection));
            return validCells;

        }

        public override void MoveToOtherCell(ChessCell nextCell)
        {
            base.MoveToOtherCell(nextCell); // normal move plus setting firstMove to false

            IsFirstMove = false;
        }

        public override string ToString()
        {
            return "P";
        }

    }
}
