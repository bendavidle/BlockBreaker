using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBreaker
{
    internal class Helper
    {
        private static object lockobject = new object();

        public static void PrintAtPosition(int x, int y, char symbol)
        {
            lock (lockobject)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(symbol);
            }
        }
    }
}
