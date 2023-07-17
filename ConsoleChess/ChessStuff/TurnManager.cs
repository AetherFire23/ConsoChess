using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.ChessStuff
{
    public class TurnManager
    {
        public bool IsLocalTeamTurn = true;

        public void SwapTurn()
        {
            IsLocalTeamTurn = !IsLocalTeamTurn;
        }

        public bool IsCorrectPlayerTurn(Piece piece)
        {
            bool isCorrect = piece.IsLocalTeam == IsLocalTeamTurn;
            return isCorrect;
        }
    }
}
