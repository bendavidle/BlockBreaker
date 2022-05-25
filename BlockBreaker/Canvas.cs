using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int _moveRate;
        private int _drawRate = 20;
        private bool isDrawn = false;
        private string _scene;

        public Canvas(int height, int width, int x, int y)
        {
            Height = height;
            Width = width;
            X = x;
            Y = y;
            Paddle = new Paddle(this);
            balls = new List<Ball> { new(this, Paddle) };
            CreateScene();
        }

        public void Show()
        {

            Console.CursorVisible = false;

            _drawRate = (_drawRate + 1) % 20;
            bool draw = _drawRate == 0;

            //Console.Clear();

            _moveRate = (_moveRate + 1) % 2;
            MoveBall(_moveRate != 0);
            Paddle.Show();

            if (!isDrawn)
            {
                DrawCanvas();
                isDrawn = true;
            }

            //if (draw)
            //{
            //    Console.Clear();
            //    DrawCanvas();
            //}
        }

        private void DrawCanvas()
        {

            for (int col = 0; col < Height; col++)
            {
                Helper.PrintAtPosition(0 + X, col + Y, 'x');
                Helper.PrintAtPosition(Width + X, col + Y, 'x');

                //Console.SetCursorPosition(0 + X, col + Y);
                //Console.Write("x");
                //Console.SetCursorPosition(Width + X, col + Y);
                //Console.Write("x");
            }

            for (int row = 0; row < Width; row++)
            {
                Helper.PrintAtPosition(row + X, 0 + Y, 'x');
                Helper.PrintAtPosition(row + X, Height + Y, 'x');

                //Console.SetCursorPosition(row + X, 0 + Y);
                //Console.Write("x");
                //Console.SetCursorPosition(row + X, Height + Y);
                //Console.Write("x");
            }
        }

        private void CreateScene()
        {
            for (int row = 0; row < Width; row++)
            {
                for (int col = 0; col < Height; col++)
                {

                    if (row == 0 || row == Width)
                    {
                        _scene += "x";
                    }
                    else
                    {
                        _scene += " ";
                    }

                    if (col == 0 || row == Height)
                    {
                        _scene += "x";
                    }
                    else
                    {
                        _scene += " ";
                    }
                }
                _scene += '\n';
            }
        }


        public void RemoveBall(Ball ball)
        {
            balls.Remove(ball);
        }

        public void MoveBall(bool blank)
        {
            if (balls.Count > 0)
            {
                foreach (Ball ball in balls.ToList())
                {
                    ball.Show(blank);
                }
            }
        }


        public void SpawnBall()
        {
            balls.Add(new Ball(this, Paddle));
        }
    }
}
