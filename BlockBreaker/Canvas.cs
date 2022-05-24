using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBreaker
{
    internal class Canvas
    {
        public int X { get; }
        public int Y { get; }
        public int Height { get; }
        public int Width { get; }
        public Paddle Paddle { get; }
        public List<Ball> balls { get; }
        private bool _moveBall = true;
        private int test;

        public Canvas(int height, int width, int x, int y)
        {
            Height = height;
            Width = width;
            X = x;
            Y = y;
            Paddle = new Paddle(this);
            balls = new List<Ball> { new(this, Paddle) };
        }

        public void Show()
        {
            Console.Clear();
            for (int col = 0; col < Height; col++)
            {
                Console.SetCursorPosition(0 + X, col + Y);
                Console.Write("x");
                Console.SetCursorPosition(Width + X, col + Y);
                Console.Write("x");
            }

            for (int row = 0; row < Width; row++)
            {
                Console.SetCursorPosition(row + X, 0 + Y);
                Console.Write("x");
                Console.SetCursorPosition(row + X, Height + Y);
                Console.Write("x");
            }

            Paddle.Show();


            test = (test + 1) % 4;
            MoveBall(test != 0);
            //_moveBall = !_moveBall;


        }
        public void RemoveBall(Ball ball)
        {
            balls.Remove(ball);
        }

        private void MoveBall(bool blank)
        {
            if (balls.Count > 0)
            {
                foreach (Ball ball in balls.ToList())
                {
                    ball.Show(blank);
                }
            }
        }


    }
}
