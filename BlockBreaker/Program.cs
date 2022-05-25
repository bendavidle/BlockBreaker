
using BlockBreaker;
using System.Runtime.InteropServices;

[DllImport("user32.dll")]
static extern short GetAsyncKeyState(int key);

var canvas = new Canvas(30, 90, 10, 5);

while (true)
{
    Console.CursorVisible = false;
    canvas.Show();
    if (Console.KeyAvailable)
    {

        if ((GetAsyncKeyState(0x25) & 0x8000) > 0)
        {
            canvas.Paddle.MoveLeft();
        }

        if ((GetAsyncKeyState(0x27) & 0x8000) > 0)
        {
            canvas.Paddle.MoveRight();
        }

        //ConsoleKeyInfo key = Console.ReadKey(true);
        //switch (key.Key)
        //{
        //    case ConsoleKey.RightArrow:
        //        {
        //            canvas.Paddle.MoveRight();
        //            break;
        //        }
        //    case ConsoleKey.LeftArrow:
        //        {
        //            canvas.Paddle.MoveLeft();
        //            break;
        //        }
        //    default:
        //        break;
        //}
    }
    Thread.Sleep(8);

}

