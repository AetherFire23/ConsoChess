using LandmassTests;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleChess.ChessStuff
{
    public class ChessGrid : GridLayout<ChessCell>
    {
        public ChessGrid(int x, int y) : base(x, y)
        {
        }

        public void PrintChessBoard(List<ChessCell> targetsToShow = null)
        {
            Console.Clear();
            if (targetsToShow is null)
            {
                targetsToShow = new List<ChessCell>();
            }
            for (int y = 0; y < this.Height; y++)
            {
                Console.Write($"{y}   ");

                for (int x = 0; x < this.Width; x++)
                {
                    var currCell = this.Cells[x][y];

                    string text = "X";
                    ConsoleColor color = ConsoleColor.White;
                    if (targetsToShow.Contains(currCell))
                    {
                        color = ConsoleColor.Red;
                        if (currCell.Piece is not null)
                        {
                            text = currCell.Piece.ToString();
                        }
                    }
                    else
                    {
                        if (currCell.Piece is not null)
                        {
                            text = currCell.Piece.ToString();
                            color = currCell.Piece.Color;
                        }

                    }

                    Console.ForegroundColor = color;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.Write("    ");
            for (int x = 0; x < this.Width; x++)
            {
                Console.Write($"{x} ");
            }
        }

        public void PrintChessBoardWithTargets(Piece piece, List<ChessCell> targetsToShow)
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    var currCell = this.Cells[x][y];
                    if (targetsToShow.Contains(currCell))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    if (piece.Cell == currCell)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    string toWrite = currCell.Piece is not null
                        ? currCell.Piece.ToString()
                        : "X";
                    Console.Write(toWrite);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }
    }
}
