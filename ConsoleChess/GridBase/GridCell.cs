using ConsoleChess.ChessStuff;
using ConsoleChess.GridBase;
using System;
using System.Collections.Generic;

namespace LandmassTests
{
    public class GridCell<T> where T : GridCell<T>
    {
        public Dictionary<Direction, T> Neighbors { get; set; } = new();
        public Dictionary<Direction, T> DoorConnections { get; set; } = new();
        public CellPosition Position;
        public int X => Position.X;
        public int Y => Position.Y;

        // requires parameterless for fucking newable or something fcu kyou
        public GridCell()
        {
            
        }
        public GridCell(int x, int y)
        {
            Position = new CellPosition(x, y);
        }

        public void ConnectNeighbors(T roomCell, Direction direction)
        {
            this.Neighbors.Add(direction, roomCell);

            Direction oppositeDirection = DirectionHelper.GetOppositeDirection(direction);
            roomCell.Neighbors.Add(oppositeDirection, (T)this);
        }
    }
}
