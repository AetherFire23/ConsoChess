using ConsoleChess.ChessStuff;
using ConsoleChess.GridBase;

TurnManager turnManager = new TurnManager();

var layout = new ChessGrid(8, 8);
var localPieces = PiecesInitializer.InitLocalPieces(layout);
var enemyPieces = PiecesInitializer.InitOppositePieces(layout);


var allpieces = localPieces.Union(enemyPieces).ToList();

while (true)
{
    (bool IsCheckmated, King King) = GetCheckmatedKingOrDefault(allpieces);
    if (IsCheckmated)
    {
        Console.Clear();
        Console.WriteLine($"checkmate : king of color {King.Color} has lost ");
        Console.ReadLine();
    }

    // ask to select piece
    layout.PrintChessBoard();
    var selectedCell = InputCellPosition("enter a piece position (x,y)");
    if (selectedCell.Piece is null) continue;
    if (!turnManager.IsCorrectPlayerTurn(selectedCell.Piece)) continue;

    // ask to select move target
    var validMoves = selectedCell.Piece.GetValidCellMoves(allpieces);
    layout.PrintChessBoard(validMoves);
    if (!validMoves.Any()) continue;

    // eat or move target
    var moveCell = InputCellPosition($"enter a move pos for {selectedCell.Piece}");
    if (!validMoves.Contains(moveCell)) continue;

    selectedCell.Piece.MoveToOtherCell(moveCell);
    turnManager.SwapTurn();
}

static bool IsKingCheckmated(King someKing, List<Piece> allPiecesParam)
{
    var enemyPieces = allPiecesParam.Where(x => someKing.IsOppositeTeamPiece(x)).ToList();
    List<ChessCell> validTargetsOfEnemyPieces = enemyPieces
        .Select(x => x.ValidCellsIncludingFriendlyAndFirstBlockingPiece).SelectMany(x => x).ToList();


    bool isCheckedKing = validTargetsOfEnemyPieces.Contains(someKing.Cell);
    bool isCheckmated = isCheckedKing // king positions and all its neighbors are contained in valid moves
        && someKing.Cell.Neighbors.All(n => validTargetsOfEnemyPieces.Contains(n.Value));

    return isCheckmated;
}


ChessCell InputCellPosition(string message)
{
    bool isValidInput = false;

    while (!isValidInput)
    {
        int[] coordinates = new int[2];
        try
        {
            Console.WriteLine(message);
            coordinates = Console.ReadLine()
                .Split(',')
                .Select(x => Convert.ToInt32(x))
                .ToArray();

            if (coordinates.Any(x => x > 9)) continue;
        }
        catch
        {
            continue;
        }

        CellPosition inputPosition = new CellPosition(coordinates[0], coordinates[1]);
        var cellAtInput = layout.GetCellAtPosition(inputPosition);

        return cellAtInput;
    }

    return null;
}

static void PrintValidMoveCoords(List<ChessCell> move)
{
    move.ForEach(x => Console.WriteLine($"{x.X}{x.Y}"));
}

static void CapturePiece(Piece capturingPiece, Piece capturedPiece)
{
    capturingPiece.MoveToOtherCell(capturedPiece.Cell);
}

static (bool IsCheckmated, King King) GetCheckmatedKingOrDefault(List<Piece> allPieces)
{
    var kings = allPieces.Where(x => x is King).ToList();

    foreach (King king in kings)
    {
        if (IsKingCheckmated(king, allPieces)) return (true, king);
    }

    return (false, null);
}