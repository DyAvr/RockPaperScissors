using System.Text;
using Task3;

const string help = "?";
var argsLength = args.Length;
var game = new Game(args);
ValidateArguments();
while(!game.IsTerminated)
    Play();

void Play() {
    var hmacGenerator = new HMACGenerator(args);
    WriteInfo(hmacGenerator);
    var s = Console.ReadLine();
    if(s != help)
        UsePlayerMove(s, hmacGenerator);
    else
        game.Help();
}

void UsePlayerMove(string? move, HMACGenerator generator) {
    if(int.TryParse(move, out var playerChoice) && playerChoice > 0 && playerChoice <= argsLength)
        game.StartMove(playerChoice, generator);
    else if(playerChoice == 0)
        game.Exit();
    else
        Console.WriteLine("Your move was incorrect!\n");
}
void WriteInfo(HMACGenerator generator) {
    StringBuilder output = new StringBuilder($"HMAC: {generator.HMAC}\nAvailable moves:");
    foreach(var move in args)
        output.Append($"\n{Array.FindIndex(args, v => move == v) + 1} - {move}");
    output.Append("\n0 - Exit\n? - Help\nEnter your move: ");
    Console.Write(output);
}

void ValidateArguments() {
    if(argsLength < 2 || argsLength % 2 == 0 || args.Distinct().Count() != argsLength) {
        Console.WriteLine("The arguments were incorrect!");
        game.Exit();
    }
}