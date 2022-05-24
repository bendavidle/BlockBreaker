
using BlockBreaker;


var canvas = new Canvas(30, 90, 10, 5);

while (true)
{
    Console.CursorVisible = false;
    canvas.Show();
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.RightArrow:
                {
                    canvas.Paddle.MoveRight();
                    break;
                }
            case ConsoleKey.LeftArrow:
                {
                    canvas.Paddle.MoveLeft();
                    break;
                }
            default:
                break;
        }
    }
    Thread.Sleep(8);

}

