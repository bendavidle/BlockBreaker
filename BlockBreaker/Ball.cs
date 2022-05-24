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
        public int Angle = 1;


        public Ball(Canvas canvas, Paddle paddle)
        {
            _canvas = canvas;
            _paddle = paddle;
            X = _paddle.X + _paddle.Length / 2;
            Y = _paddle.Y - 1;
        }

        public void Show()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("O");

            //Collision Top and Bottom
            if (Y + Direction == _canvas.Y)
            {
                Direction = 1;
            }

            if (Y + Direction == _canvas.Height + _canvas.Y)
            {
                _canvas.RemoveBall(this);
            }

            if (Y + Direction == _paddle.Y && HitPaddle())
            {
                Direction = -1;
            }



            //Collision Wall
            if (X + Angle == _canvas.X || X + Angle == _canvas.X + _canvas.Width)
            {
                Angle = -Angle;
            }


            Y += Direction;
            X += Angle;
        }

        private bool HitPaddle()
        {
            for (int i = _paddle.X; i < _paddle.X + _paddle.Length; i++)
            {
                if (X == i)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

