using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.ChessStuff;
public class MoveInfo
{
    public List<Piece> FriendlyPieces;
    public List<Piece> EnemyPieces;
    public King FriendlyKing;

    public List<Piece> EnemyPiecesWithoutKing => EnemyPieces.Where(x=> x is not King).ToList();
    public List<Piece> FriendlyiecesWithoutKing => FriendlyPieces.Where(x=> x is not King).ToList();

    public MoveInfo(List<Piece> allPieces, Piece currentPiece)
    {
        FriendlyPieces = allPieces.Where(x => !currentPiece.IsOppositeTeamPiece(x)).ToList();
        FriendlyKing = FriendlyPieces.First(x => x is King) as King;
        EnemyPieces = allPieces.Where(x => currentPiece.IsOppositeTeamPiece(x)).ToList();
    }
}
