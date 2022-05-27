using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlockBreaker
{
    internal class Canvas
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        private int Lives { get; set; }
        public Paddle Paddle { get; private set; } = null!;
        public ScoreCounter Score { get; private set; } = null!;
        public List<Ball> Balls { get; private set; } = null!;
        public List<Block> Blocks { get; private set; } = null!;
        public bool Alive { get; private set; }
        private int _moveRate;
        private bool _isDrawn;

        public Canvas(int height, int width, int x, int y)
        {
            Init(height, width, x, y);
        }

        private void Init(int height, int width, int x, int y)
        {
            Height = height;
            Width = width;
            X = x;
            Y = y;
            Alive = true;
            _isDrawn = false;
            Lives = 3;

            Paddle = new Paddle(this);
            Score = new ScoreCounter(this);
            Balls = new List<Ball> { new(this, Paddle) };
            Blocks = new List<Block>();
            CreateBlocks(0);
        }

        public void StartGame()
        {
            [DllImport("user32.dll")]
            static extern short GetAsyncKeyState(int key);

            while (Alive)
            {

                if (Console.KeyAvailable)
                {

                    if ((GetAsyncKeyState(0x25) & 0x8000) > 0)
                    {
                        Paddle.MoveLeft();
                    }

                    if ((GetAsyncKeyState(0x27) & 0x8000) > 0)
                    {
                        Paddle.MoveRight();
                    }

                    if ((GetAsyncKeyState(0x26) & 0x8000) > 0)
                    {
                        SpawnBall();
                    }
                }
                Thread.Sleep(1000 / 60);
                Show();
            }
        }


        public void Show()
        {
            Console.CursorVisible = false;

            _moveRate = (_moveRate + 1) % 2;
            MoveBall(_moveRate != 0);
            DrawBlocks();
            Score.ShowScore();
            ShowLives();
            Paddle.Show();

            if (!_isDrawn)
            {
                DrawCanvas();
                _isDrawn = true;
            }

        }

        private void DrawCanvas()
        {

            for (int col = 0; col < Height; col++)
            {
                Helper.PrintAtPosition(0 + X, col + Y, 'x');
                Helper.PrintAtPosition(Width + X, col + Y, 'x');


            }

            for (int row = 0; row < Width; row++)
            {
                Helper.PrintAtPosition(row + X, 0 + Y, 'x');
                Helper.PrintAtPosition(row + X, Height + Y, 'x');

            }
        }

        private void CreateBlocks(int additionalRows)
        {
            int amountOfBlocks = (Width - 1) / 5;

            int row = 6 + additionalRows;
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
            if (Balls.Count == 0)
            {
                Lives--;
                if (Lives == 0)
                {
                    Alive = false;
                }
                else
                {
                    SpawnBall();
                }
            }
        }

        public void RemoveBlock(Block block)
        {
            Blocks.Remove(block);
            block.RemoveBlock();

            Score.SetScore(Score.Score + 100);

            if (Blocks.Count == 0)
            {
                CreateBlocks(1);
            }

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

        public void Reset()
        {
            Init(Height, Width, X, Y);

        }

        public void ShowLives()
        {
            //Extra Balls: O O O
            StringBuilder sb = new StringBuilder();
            sb.Append("Extra Balls: ");
            for (int i = 1; i < Lives; i++)
            {
                sb.Append("O ");
            }

            Helper.PrintAtPosition(X + Width - 18, Y - 2, "                    ");
            Helper.PrintAtPosition(X + Width - 17, Y - 2, sb.ToString());
        }
    }
}
