using ConsoleChess.GridBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LandmassTests
{
    public class GridLayout<TCell> where TCell : GridCell<TCell>, new()
    {
        public List<List<TCell>> Cells { get; set; }
        public List<TCell> AllCells => Cells.SelectMany(x => x).ToList();

        public int Width;
        public int Height;

        public GridLayout(int width, int height)
        {
            Cells = new();
            this.Width = width;
            this.Height = height;
            InitEmptyLayout(width, height);
            ConnectAllRoomsCells();
            // PrintBoard();
        }

        public void InitEmptyLayout(int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                var horizontalRooms = new List<TCell>();
                Cells.Add(horizontalRooms);

                for (int y = 0; y < height; y++)
                {
                    TCell roomCell = new();
                    roomCell.Position = new CellPosition(x, y);

                    horizontalRooms.Add(roomCell);
                }
            }
        }

        public void ConnectAllRoomsCells()
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    var currentCell = GetCellAtPosition(x, y);
                    ConnectCell(currentCell);
                }
            }
        }

        public void ConnectCell(GridCell<TCell> currentCell)
        {
            bool isFirstColumn = currentCell.Position.X == 0;
            bool isLastColumn = currentCell.Position.X == this.Cells.Count - 1;
            bool isFirstRow = currentCell.Position.Y == 0;

            if (!isFirstColumn)
            {
                var westCell = this.Cells[currentCell.X - 1][currentCell.Y];
                currentCell.ConnectNeighbors(westCell, Direction.West);
            }

            if (!isFirstRow)
            {
                var northCell = this.Cells[currentCell.X][currentCell.Y - 1];
                currentCell.ConnectNeighbors(northCell, Direction.North);

                if (!isFirstColumn)
                {
                    var northWestCell = this.Cells[currentCell.X - 1][currentCell.Y - 1];
                    currentCell.ConnectNeighbors(northWestCell, Direction.NorthWest);
                }

                if (!isLastColumn)
                {
                    var northEastCell = this.Cells[currentCell.X + 1][currentCell.Y - 1];
                    currentCell.ConnectNeighbors(northEastCell, Direction.NorthEast);
                }
            }
        }

        public TCell GetCellAtPosition(int x, int y)
        {
            var cell = GetCellAtPosition(new CellPosition(x, y));
            return cell;
        }

        public TCell GetCellAtPosition(CellPosition position)
        {
            var cell = Cells[position.X][position.Y];
            return cell;
        }

        public void PrintNeighbors()
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    var currCell = this.Cells[x][y];
                    Console.WriteLine("sex");
                    foreach (var cell in currCell.Neighbors)
                    {
                        Console.WriteLine(cell.Key);
                    }
                }
            }
        }

        public void PrintBoard()
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    var currCell = this.Cells[x][y];
                    Console.Write("X");
                }
                Console.WriteLine("");
            }
        }
    }
}
