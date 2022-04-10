using ConsoleTables;

namespace Task3;
public class HelpTable {
    private ConsoleTable? table;
    private readonly Game game;

    public HelpTable(Game game) {
        this.game = game;
        this.table = null;
    }
    public ConsoleTable GetTable() {
        if(table == null)
            Initialize();
        return table!;
    }

    private void Initialize() {
        table = new ConsoleTable(CreateColumns()) {
            Options = {
                EnableCount = false
            }
        };
        CreateRows();
    }

    private string[] CreateColumns() {
        var columns = new List<string> { "Your move\\Computer move" };
        columns.AddRange(game.Moves);
        return columns.ToArray();
    }

    private void CreateRows() {
        foreach(var moveName in game.Moves) {
            table?.AddRow(CreateRow(moveName));
        }
    }

    private object[] CreateRow(string moveName) {
        var row = new List<object> { moveName };
        row.AddRange(game.Moves
            .ToList<string>()
            .Select(m => (object)game.
                GetResult(Array.FindIndex(game.Moves, v => moveName == v) + 1,
                    Array.FindIndex(game.Moves, v => m == v) + 1)));
        return row.ToArray();
    }
}