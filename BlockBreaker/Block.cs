using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBreaker
{
    internal class Block
    {


        public int X { get; private set; }
        public int Y { get; private set; }
        public string? Shape { get; private set; }

        public Block(int x, int y)
        {
            X = x;
            Y = y;
            Shape = CreateShape();
        }

        public void DrawBlock()
        {
            string[] splitShape = Shape.Split("\n");
            for (int i = 0; i < splitShape.Length; i++)
            {
                Helper.PrintAtPosition(X, Y + i, splitShape[i]);
            }
        }

        public void RemoveBlock()
        {
            string[] splitShape = Shape.Split("\n");
            for (int i = 0; i < splitShape.Length; i++)
            {
                Helper.PrintAtPosition(X, Y + i, "     ");
            }
        }

        private static string? CreateShape()
        {
            StringBuilder shape = new StringBuilder();
            shape.AppendLine("┌───┐");
            shape.AppendLine("└───┘");

            return shape.ToString();
        }
    }
}
//"┌───┐\n" +
//"└───┘"