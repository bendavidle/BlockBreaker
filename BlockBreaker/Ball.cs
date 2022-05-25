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
                
                Console.SetCursorPosition(X, Y);
                Console.Write("O");
                Console.SetCursorPosition(X-Angle,Y - Direction );
                Console.Write(" ");
                
                return;

            }
            
            Console.SetCursorPosition(X, Y);
            Console.Write("O");
            Console.SetCursorPosition(X - Angle, Y - Direction);
            Console.Write(" ");


            //Collision Top and Bottom
            if (Y + Direction == _canvas.Y || Y + Direction == _paddle.Y && HitPaddle())
            {
                Direction = -Direction;
            }

            if (Y + Direction == _canvas.Height + _canvas.Y)
            {
                _canvas.RemoveBall(this);
            }



            //Collision Wall
            if (X + Angle <= _canvas.X || X + Angle >= _canvas.X + _canvas.Width)
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


                if (X + Angle == i)
                {
                    if (i < _paddle.X + _paddle.Length / 2)
                    {
                        Angle--;
                    }

                    if (i > _paddle.X + _paddle.Length / 2)
                    {
                        Angle++;
                    }
                    return true;
                }

            }

            return false;
        }

    }

}

