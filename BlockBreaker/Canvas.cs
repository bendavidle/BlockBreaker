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
        public List<Ball> Balls { get; }
        public List<Block> Blocks { get; }
        private int _moveRate;
        private int _drawRate = 20;
        private bool _isDrawn = false;

        public Canvas(int height, int width, int x, int y)
        {
            Height = height;
            Width = width;
            X = x;
            Y = y;
            Paddle = new Paddle(this);
            Balls = new List<Ball> { new(this, Paddle) };
            Blocks = new List<Block>();
            CreateBlocks();
        }



        public void Show()
        {
            Console.CursorVisible = false;

            _drawRate = (_drawRate + 1) % 20;
            bool draw = _drawRate == 0;

            //Console.Clear();

            _moveRate = (_moveRate + 1) % 2;
            MoveBall(_moveRate != 0);
            DrawBlocks();
            Paddle.Show();

            if (!_isDrawn)
            {
                DrawCanvas();
                _isDrawn = true;
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

        private void CreateBlocks()
        {
            int amountOfBlocks = (Width - 1) / 5;

            int row = 6;
            for (int j = 0; j < row; j++)
            {
                for (int i = 0; i < amountOfBlocks; i++)
                {
                    int blockX = X + 1 + 5 * i;
                    int blockY = Y + 4 + 2 * j;
                    Blocks.Add(new Block(blockX, blockY));

                }
            }

        }

        public void RemoveBall(Ball ball)
        {
            Balls.Remove(ball);
        }

        public void RemoveBlock(Block block)
        {
            Blocks.Remove(block);
            block.RemoveBlock();

        }

        public void MoveBall(bool blank)
        {
            if (Balls.Count > 0)
            {
                foreach (Ball ball in Balls.ToList())
                {
                    ball.Show(blank);
                }
            }
        }

        public void DrawBlocks()
        {
            if (Blocks.Count > 0)
            {
                foreach (Block block in Blocks.ToList())
                {
                    block.DrawBlock();
                }
            }
        }


        public void SpawnBall()
        {
            Balls.Add(new Ball(this, Paddle));
        }
    }
}

// arr [["x,y""x+1, y"..."x+4, y""x,y+1""x+1, y+1"..."x+4, y+1"] ["x,y""x+1, y"..."x+4, y""x,y+1""x+1, y+1"..."x+4, y+1"]]
