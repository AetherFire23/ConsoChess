using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandmassTests
{
    public enum Direction
    {
        South,
        North,
        West,

        East,

        NorthWest,
        NorthEast,
        SouthWest,
        SouthEast,
    }

    public static class DirectionHelper
    {
        public static Direction IntToHorizontal(int integer)
        {
            if (integer < 0)
            {
                return Direction.West;
            }
            else if (integer > 0)
            {
                return Direction.East;
            }
            else
            {
                return Direction.West;
            }
        }

        public static Direction IntToVertical(int integer)
        {
            if (integer < 0)
            {
                return Direction.South;
            }
            else if (integer > 0)
            {
                return Direction.North;
            }
            else
            {
                return Direction.West;
            }
        }

        public static Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.South: return Direction.North;
                case Direction.North: return Direction.South;

                case Direction.West: return Direction.East;
                case Direction.East: return Direction.West;

                case Direction.NorthWest: return Direction.SouthEast;
                case Direction.NorthEast: return Direction.SouthWest;

                case Direction.SouthWest: return Direction.NorthEast;
                case Direction.SouthEast: return Direction.NorthWest;

                default: throw new Exception("error");
            }
        }

        public static List<Direction> GetDiagonalDirections()
        {
            var diagonalsDirections = new List<Direction>
            {
                Direction.NorthWest,
                Direction.NorthEast,
                Direction.SouthEast,
                Direction.SouthWest,
            };
            return diagonalsDirections;
        }

        public static List<Direction> GetHorizontalAndVerticalDirections()
        {
            var lineDirections = new List<Direction>
            {
                Direction.West,
                Direction.East,
                Direction.North,
                Direction.South,
            };
            return lineDirections;
        }


        public static List<Direction> GetAllDirections()
        {
            var values = ((Direction[])Enum.GetValues(typeof(Direction))).ToList();
            return values;
        }

        public static bool IsEastern(Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    {
                        return true;
                    }
                case Direction.NorthEast:
                    {
                        return true;

                    }
                case Direction.SouthEast:
                    {
                        return true;

                    }
            }
            return false;
        }

        public static bool IsWestern(Direction direction)
        {
            switch (direction)
            {
                case Direction.West:
                    {
                        return true;
                    }
                case Direction.NorthWest:
                    {
                        return true;

                    }
                case Direction.SouthWest:
                    {
                        return true;

                    }
            }
            return false;
        }
    }
}
