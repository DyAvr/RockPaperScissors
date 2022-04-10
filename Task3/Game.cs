namespace Task3;

public class Game {
    private string[] moves;
    private HelpTable helpTable;
    private HMACGenerator generator;
    public string[] Moves => moves;
    public bool IsTerminated { get; private set; }

    public Game(string[] moves) {
        this.moves = moves;
        this.helpTable = new HelpTable(this);
    }

    public void StartMove(int playerChoice, HMACGenerator generator) {
        if(IsTerminated)
            throw new OperationCanceledException("The game was terminated");
        this.generator = generator;
        var computerChoice = generator.Move;
        var result = GetResult(playerChoice, computerChoice);
        PrintResult(playerChoice, computerChoice, result);
    }

    public void Exit() {
        IsTerminated = true;
    }
    
    public void Help() {
        if(IsTerminated)
            throw new OperationCanceledException("The game was terminated");
        Console.WriteLine(helpTable.GetTable());
    }

    public MoveResult GetResult(int playerMove, int computerMove) {
        if(playerMove == computerMove)
            return MoveResult.Draw;
        if(computerMove < playerMove)
            computerMove += moves.Length;
        return computerMove <= playerMove + moves.Length / 2 ? MoveResult.Lose : MoveResult.Win;
    }

    private void PrintResult(int playerChoice, int computerChoice, MoveResult result) {
        var status = (result == MoveResult.Draw ? "It's a " : "You ") +
                     result.ToString().ToLower() + "!";
        Console.WriteLine($"Your move: {moves[playerChoice - 1]}\n" +
                          $"Computer move: {moves[computerChoice - 1]}\n" +
                          status +
                          $"\nHMAC key: {generator.HMACKey}\n");
    }
}