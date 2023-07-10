using Microsoft.Extensions.Logging;


namespace ChessBrowser;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif


        Console.WriteLine("Start");
        //List<ChessGame> chessgame = PgnReader.readFile("/Users/howard/MSD-2022-2023/CS6016/Lab6_7_Chess_Browser/ChessBrowser/kb1.pgn");
        //Console.WriteLine(chessgame.Count);
        //foreach (ChessGame game in chessgame)
        //{
        //    Console.WriteLine($"Event: {game.eventName}");
        //    Console.WriteLine($"Site: {game.site}");
        //    Console.WriteLine($"Round: {game.round}");
        //    Console.WriteLine($"White: {game.white}");
        //    Console.WriteLine($"Black:  {game.black}");
        //    Console.WriteLine($"WhiteElo: {game.whiteElo}");
        //    Console.WriteLine($"BlackElo:  {game.blackElo}");
        //    Console.WriteLine($"Result: {game.result} ");
        //    Console.WriteLine($"EventDate: {game.eventDate}");
        //    Console.WriteLine("------------------------");
        //}
        return builder.Build();
    }
}
