using LandmassTests;

namespace ConsoleChess.ChessStuff
{
    public class King : Piece
    {

        public King(ChessCell startingCell, bool isLocalTeam) : base(startingCell, isLocalTeam)
        {
        }

        // I have a problem where the king thinks that moving into an enemy piece protected by another enemy piece is valid.
        // I need the pieces without friendly exclusion
        public override List<(Direction direction, List<ChessCell> Cells)> GetObservedDirectionsAndCells()
        {
            var observed = DirectionHelper
                .GetAllDirections()
                // weird shit because i need to cast everything to a tuple 
                .Select(dir => (dir, new List<ChessCell> { Cell.GetCellForDistanceOrDefault(dir, 1) }))
                .Where(x => !x.Item2.Any(x => x is null))
                .ToList();
            return observed;
        }

        public override List<ChessCell> GetValidCellMoves(List<Piece> allPieces)
        {
            var validFoReal = GetObservedDirectionsAndCells().Select(x => x.Cells)
                .SelectMany(x => x).ToList()
                .FilterFriendlyPieces(this)
                .Where(x => x is not null).ToList();

            // cant move on protected pieces ! FUCKING WORKS NIGGER
            var enemyPiecesIncludeFirstPiece = allPieces.Where(x => IsOppositeTeamPiece(x))
                .Select(x => x.GetObservedPiecesForEachDirection()).SelectMany(x => x)
                .Where(x => x.Any())
                .Select(x => x.First().Cell).ToList();

            // dernier probleme : 

            var linesAfterKing = allPieces.Where(x => IsOppositeTeamPiece(x))
                .Select(x => x.GetLineOfCellsObservingOppositeKing()).SelectMany(x => x).ToList();

           // validFoReal.AddRange(linesAfterKing);

            foreach (var move in enemyPiecesIncludeFirstPiece)
            {
                validFoReal.Remove(move);
            }
            foreach (var lin in linesAfterKing)
            {
                validFoReal.Remove(lin);
            }

            return RestrictPieceMovementDueToPinsOrKingCheck(allPieces, validFoReal);
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
