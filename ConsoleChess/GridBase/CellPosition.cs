using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.GridBase
{
    public class CellPosition
    {
        public int X;
        public int Y;
        public CellPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {

            CellPosition other = (CellPosition)obj;
            bool isEqual = this.X == other.X && this.Y == other.Y;
            return isEqual;
        }
    }
}
