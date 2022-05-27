using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BlockBreaker
{
    internal class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        private readonly Canvas _canvas;
        private readonly Paddle _paddle;
        //Vector
        public int Direction = -1;
        public int Angle = -1;


        public Ball(Canvas canvas, Paddle paddle)
        {
            _canvas = canvas;
            _paddle = paddle;
            X = _paddle.X + _paddle.Length / 2;
            Y = _paddle.Y - 1;
        }

        public void Show(bool blank)
        {
            if (blank)
            {
                Helper.PrintAtPosition(X, Y, 'O');
                Helper.PrintAtPosition(X - Angle, Y - Direction, ' ');

                //Console.SetCursorPosition(X, Y);
                //Console.Write("O");
                //Console.SetCursorPosition(X - Angle, Y - Direction);
                //Console.Write(" ");

                return;

            }

            Helper.PrintAtPosition(X, Y, 'O');
            Helper.PrintAtPosition(X - Angle, Y - Direction, ' ');

            //Console.SetCursorPosition(X, Y);
            //Console.Write("O");
            //Console.SetCursorPosition(X - Angle, Y - Direction);
            //Console.Write(" ");

            //Collision Top and Bottom
            if (Y + Direction <= _canvas.Y || Y + Direction >= _paddle.Y && HitPaddle())
            {
                Direction = -Direction;
            }

            if (Y + Direction == _canvas.Height + _canvas.Y)
            {
                _canvas.RemoveBall(this);
                Helper.PrintAtPosition(X, Y, ' ');
            }



            //Collision Wall
            if (X + Angle <= _canvas.X || X + Angle >= _canvas.X + _canvas.Width)
            {
                Angle = -Angle;
            }


            if (HitBlock() != null)
            {
                Block hitBlock = HitBlock();

                if (HitBlockSide(hitBlock))
                {
                    Angle = -Angle;
                }
                else
                {
                    Direction = -Direction;
                }

                _canvas.RemoveBlock(hitBlock);
            }

            Y += Direction;
            X += Angle;


        }

        private bool HitPaddle()
        {
            for (int i = _paddle.X; i < _paddle.X + _paddle.Length; i++)
            {


                if (X + Angle == i)
                {
                    if (i < _paddle.X + _paddle.Length / 2)
                    {
                        if (Angle - 1 == -2)
                        {
                            Angle = -1;
                        }
                        else
                        {
                            Angle--;
                        }
                    }

                    if (i > _paddle.X + _paddle.Length / 2)
                    {
                        if (Angle + 1 == 2)
                        {
                            Angle = 1;
                        }
                        else
                        {
                            Angle++;
                        }
                    }
                    return true;
                }

            }

            return false;
        }


        private Block HitBlock()
        {
            foreach (Block block in _canvas.Blocks)
            {

                List<int[]> blockPositions = new List<int[]>();

                for (int i = block.Y; i < block.Y + 2; i++)
                {
                    for (int j = block.X; j < block.X + 5; j++)
                    {
                        int[] coordinate = { j, i };
                        blockPositions.Add(coordinate);
                    }
                }

                int[] ballCoordinate = { X + Angle, Y + Direction };

                if (blockPositions.Any(p => p.SequenceEqual(ballCoordinate)))
                {

                    return block;
                }
            }

            return null!;
        }

        private bool HitBlockSide(Block hitBlock)
        {
            foreach (Block block in _canvas.Blocks)
            {

                List<int[]> blockPositions = new List<int[]>();

                for (int i = block.Y; i < block.Y + 2; i++)
                {
                    for (int j = block.X; j < block.X + 5; j++)
                    {
                        int[] coordinate = { j, i };
                        blockPositions.Add(coordinate);
                    }
                }

                int[] blockLeft = { hitBlock.X - 1, hitBlock.Y };
                int[] blockRight = { hitBlock.X + 6, hitBlock.Y };
                int[] nextBallPos = { X + Angle, Y + Direction };

                if (nextBallPos.SequenceEqual(new[] { hitBlock.X, hitBlock.Y }) || nextBallPos.SequenceEqual(new[] { hitBlock.X, hitBlock.Y + 1 }))
                {
                    if (Angle > 0)
                    {
                        return !blockPositions.Any(p => p.SequenceEqual(blockLeft));
                    }
                }
                if (nextBallPos.SequenceEqual(new[] { hitBlock.X + 4, hitBlock.Y }) || nextBallPos.SequenceEqual(new[] { hitBlock.X + 4, hitBlock.Y + 1 }))
                {
                    if (Angle < 0)
                    {
                        return !blockPositions.Any(p => p.SequenceEqual(blockRight));
                    }
                }

            }

            return false;
        }
    }

}

