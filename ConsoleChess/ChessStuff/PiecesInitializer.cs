using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.ChessStuff
{
    public class PiecesInitializer
    {
        public static King LocalKing;
        public static King OppositeKing;

        public static List<Piece> InitLocalPieces(ChessGrid grid)
        {
            int pawnRank = 6;
            int bigPiecesRank = 7;
            var initedPieces = new List<Piece>();
            foreach (int index in Enumerable.Range(0, grid.Width))
            {
                // init pawns
                var cell = grid.GetCellAtPosition(index, pawnRank);
                initedPieces.Add(new Pawn(cell, true));
            }

            initedPieces.Add(new Rook(grid.GetCellAtPosition(0, bigPiecesRank), true));
            initedPieces.Add(new Bishop(grid.GetCellAtPosition(2, bigPiecesRank), true));
            initedPieces.Add(new Knight(grid.GetCellAtPosition(1, bigPiecesRank), true));

            initedPieces.Add(new King(grid.GetCellAtPosition(3, bigPiecesRank), true));
            initedPieces.Add(new Queen(grid.GetCellAtPosition(4, bigPiecesRank), true));

            initedPieces.Add(new Knight(grid.GetCellAtPosition(6, bigPiecesRank), true));
            initedPieces.Add(new Bishop(grid.GetCellAtPosition(5, bigPiecesRank), true));
            initedPieces.Add(new Rook(grid.GetCellAtPosition(7, bigPiecesRank), true));
            LocalKing = (King)initedPieces.FirstOrDefault(x => x is King);
            return initedPieces;
        }

        public static List<Piece> InitOppositePieces(ChessGrid grid)
        {
            int pawnRank = 1;
            int bigPiecesRank = 0;
            var initedPieces = new List<Piece>();
            foreach (int index in Enumerable.Range(0, grid.Width))
            {
                // init pawns
                var cell = grid.GetCellAtPosition(index, pawnRank);
                initedPieces.Add(new Pawn(cell, false));
            }

            initedPieces.Add(new Rook(grid.GetCellAtPosition(0, bigPiecesRank), false));
            initedPieces.Add(new Bishop(grid.GetCellAtPosition(2, bigPiecesRank), false));
            initedPieces.Add(new Knight(grid.GetCellAtPosition(1, bigPiecesRank), false));

            initedPieces.Add(new Queen(grid.GetCellAtPosition(3, bigPiecesRank), false));
            initedPieces.Add(new King(grid.GetCellAtPosition(4, bigPiecesRank), false));

            initedPieces.Add(new Knight(grid.GetCellAtPosition(6, bigPiecesRank), false));
            initedPieces.Add(new Bishop(grid.GetCellAtPosition(5, bigPiecesRank), false));
            initedPieces.Add(new Rook(grid.GetCellAtPosition(7, bigPiecesRank), false));
            OppositeKing = (King)initedPieces.First(x => x is King);
            return initedPieces;
        }

        public static List<Piece> InitDummyLocal(ChessGrid grid)
        {
            int pawnRank = 6;
            int bigPiecesRank = 7;
            var initedPieces = new List<Piece>();
            //foreach (int index in Enumerable.Range(0, grid.Width))
            //{
            //    // init pawns
            //    var cell = grid.GetCellAtPosition(index, pawnRank);
            //    initedPieces.Add(new Pawn(cell, true));
            //}

            initedPieces.Add(new Rook(grid.GetCellAtPosition(0, bigPiecesRank), true));
            initedPieces.Add(new Bishop(grid.GetCellAtPosition(0, 1), true));
            initedPieces.Add(new Knight(grid.GetCellAtPosition(1, bigPiecesRank), true));

            initedPieces.Add(new King(grid.GetCellAtPosition(3, bigPiecesRank), true));
            initedPieces.Add(new Queen(grid.GetCellAtPosition(3, 2), true));

            initedPieces.Add(new Knight(grid.GetCellAtPosition(6, bigPiecesRank), true));
            initedPieces.Add(new Bishop(grid.GetCellAtPosition(5, bigPiecesRank), true));
            initedPieces.Add(new Rook(grid.GetCellAtPosition(7, bigPiecesRank), true));
            LocalKing = (King)initedPieces.FirstOrDefault(x => x is King);
            return initedPieces;
        }

        public static List<Piece> InitDummyEnemy(ChessGrid grid)
        {
            int pawnRank = 1;
            int bigPiecesRank = 0;
            var initedPieces = new List<Piece>();
            //foreach (int index in Enumerable.Range(0, grid.Width))
            //{
            //    // init pawns
            //    var cell = grid.GetCellAtPosition(index, pawnRank);
            //    initedPieces.Add(new Pawn(cell, false));
            //}

            initedPieces.Add(new Rook(grid.GetCellAtPosition(0, bigPiecesRank), false));
            initedPieces.Add(new Bishop(grid.GetCellAtPosition(2, bigPiecesRank), false));
            initedPieces.Add(new Knight(grid.GetCellAtPosition(1, bigPiecesRank), false));

            initedPieces.Add(new Queen(grid.GetCellAtPosition(4, bigPiecesRank), false));
            initedPieces.Add(new King(grid.GetCellAtPosition(3, bigPiecesRank), false));

            initedPieces.Add(new Knight(grid.GetCellAtPosition(6, bigPiecesRank), false));
            //initedPieces.Add(new Bishop(grid.GetCellAtPosition(5, bigPiecesRank), false));
            initedPieces.Add(new Rook(grid.GetCellAtPosition(7, bigPiecesRank), false));
            OppositeKing = (King)initedPieces.First(x => x is King);
            return initedPieces;
        }
    }
}
