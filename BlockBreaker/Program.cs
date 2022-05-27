
using BlockBreaker;

Canvas canvas = new Canvas(30, 91, 10, 5);

Console.CursorVisible = false;
Console.WindowHeight = 40;
Console.WindowWidth = 110;

bool isFirstGame = true;

while (true)
{
    Console.Clear();
    while (Console.KeyAvailable) { Console.ReadKey(true); }
    Console.WriteLine(isFirstGame ? "Press space to start game" : $"You scored {canvas.Score.MaxScore} points!\nPress space to try again");

    while (true)
    {
        var info = Console.ReadKey(true);
        if (info.Key == ConsoleKey.Spacebar) break;
    }

    NewGame();


    canvas.StartGame();

}

void NewGame()
{
    Console.Clear();

    isFirstGame = false;
    if (!isFirstGame)
    {
        canvas.Reset();
    }
}






