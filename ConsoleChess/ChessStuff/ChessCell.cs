using LandmassTests;

namespace ConsoleChess.ChessStuff
{
    public class ChessCell : GridCell<ChessCell>
    {
        public Piece? Piece { get; set; }
        public bool HasPiece => Piece != null;
        public ChessCell() : base()
        {
            Piece = null;
        }
        public ChessCell(int x, int y) : base(x, y)
        {
        }

        // used for jumps like knight basically or castling because it can jump
        public ChessCell GetCellRelativePositionOrDefault(Direction horizontalDirection, int xDistance, Direction verticalDirection, int yDistance)
        {
            var xCell = GetCellForDistanceOrDefault(horizontalDirection, xDistance);
            if (xCell is null) return null;

            var yCell = xCell.GetCellForDistanceOrDefault(verticalDirection, yDistance);
            return yCell;
        }

        public ChessCell? GetCellForDistanceOrDefault(Direction direction, int distance) // jumps
        {
            ChessCell currentCell = this;
            while (distance != 0)
            {
                // casting because GridCell<ChessCell> is not ChessCell, but ChessCell actually inherits from GridCell, INCELLPTION!
                currentCell = (ChessCell)currentCell.Neighbors.GetValueOrDefault(direction);
                distance--;
                if (currentCell is null) return null; // return null if it reaches end board
            }
            return currentCell;
        }

        public List<ChessCell> GetCellsInDirectionLine(Direction direction) // until reaching end of board
        {
            ChessCell currentCell = this;
            List<ChessCell> observedCells = new();
            while (currentCell != null)
            {
                currentCell = (ChessCell)currentCell.Neighbors.GetValueOrDefault(direction);
                if (currentCell is not null)
                {
                    observedCells.Add(currentCell);
                }
            }
            return observedCells;
        }

        public List<ChessCell> GetCellsInDirectionLineAndDistance(Direction direction, int distance)
        {
            ChessCell currentCell = this;
            List<ChessCell> observedCells = new();
            while (currentCell != null && distance != 0)
            {
                currentCell = (ChessCell)currentCell.Neighbors.GetValueOrDefault(direction);
                if (currentCell is not null)
                {
                    observedCells.Add(currentCell);
                    distance--;
                }
            }
            return observedCells;
        }

        public List<(Direction Direction, List<ChessCell> Cells)> GetDiagonalCellLines()
        {
            var diagonals = DirectionHelper.GetDiagonalDirections()
                .Select(d => (d, GetCellsInDirectionLine(d)))
                .ToList();
            return diagonals;
        }

        public List<(Direction Direction, List<ChessCell> Cells)> GetHorizontalAndVerticalCellLines()
        {
            var horizontalAndVertical = DirectionHelper.GetHorizontalAndVerticalDirections()
                .Select(dir => (dir, GetCellsInDirectionLine(dir)))
                .ToList();
            return horizontalAndVertical;
        }

        public List<ChessCell> AsList()
        {
            var listified = new List<ChessCell>() { this };
            return listified;
        }
    }
}
