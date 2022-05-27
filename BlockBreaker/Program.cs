
using BlockBreaker;

Canvas canvas = new Canvas(30, 91, 10, 5);

Console.CursorVisible = false;
Console.WindowHeight = 60;
Console.WindowWidth = 110;

bool isFirstGame = true;

while (true)
{
    Console.Clear();

    Console.WriteLine(isFirstGame ? "Press space to start game" : $"You scored {canvas.Score.MaxScore} points!\nPress space to try again");

    //ConsoleKey pressed;
    //do
    //{
    //    pressed = Console.ReadKey().Key;
    //} while (pressed != ConsoleKey.Spacebar);



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






