using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.ChessStuff
{
    public static class ListExtensions
    {
        // better used if it is in line or in direciton. sexy!
        public static List<ChessCell> GetCellsUntil(this List<ChessCell> self, Piece piece,
            bool mustExcludeLocalTeamPieces = true,
            bool mustIncludeEnemyPieces = true)
        {
            ChessCell? cellWithPiece = self.FirstOrDefault(x => x.Piece is not null);

            // Return the whole list because end of board reached
            if (cellWithPiece is null) return self;

            // Do not include the last piece if it is in local team
            int takeIndex = (mustExcludeLocalTeamPieces && !piece.IsOppositeTeamPiece(cellWithPiece.Piece))
                // obligatoirement enemy
                ? self.IndexOf(cellWithPiece)
                : self.IndexOf(cellWithPiece) + 1;

            return self.Take(takeIndex).ToList();
        }


        public static List<ChessCell> FilterFriendlyPieces(this List<ChessCell> self, Piece friendlyPiece)
        {
            self.RemoveAll(x => x.Piece is not null
            && !friendlyPiece.IsOppositeTeamPiece(x.Piece));

            return self;
        }
    }
}
