using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.View
{
    class Display
    {
        public void PrintMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void PrintErrorMessage(string errorMessage)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(errorMessage);
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
