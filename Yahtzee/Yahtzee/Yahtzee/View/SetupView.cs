using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.View
{
    class SetupView
    {

        private readonly string mainMenu = "Welcome to Yahtzee! \nThe rules are ... \n";
        public SetupView()
        {
            Console.WriteLine(mainMenu);
        }
        public int NumberOfPlayers()
        {
            Console.WriteLine("How many players (1-5): ");
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out int value) && value <= 5 && value >= 1)
                {
                    return value;
                }
                Console.WriteLine("Invalid input value, you need to give a value between 1 and 5.");
            }
        }

        public string PlayerName()
        {
            {
                Console.WriteLine("Player what is your name (3-8 characters): ");
                string input = Console.ReadLine().ToLower();
                if (input.Length <= 8 && input.Length >= 3)
                {
                    return input;
                }
                Console.WriteLine("Invalid input.");
            } while (true);
        }

        public bool IsRobot()
        {
            do
            {
                Console.WriteLine("Is this player a robot (y/n)");
                string input = Console.ReadLine().ToLower();
                if (input.CompareTo("y") == 0)
                {
                    Console.WriteLine("Robot created successfully");
                    return true;
                }
                else if (input.CompareTo("n") == 0)
                {
                    return false;
                }
                Console.WriteLine("Invalid input, answer with (y/n).");
            } while (true);
           
        }

    }
}
