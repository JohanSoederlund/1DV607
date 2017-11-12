using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Model.Rules;

namespace Yahtzee.View
{
    class MainMenu : Display
    {
        private readonly string welcome = "\t\t\t Welcome to ";
        private readonly string rules = "\n\tRules can be found at ";
        public GameType RenderStartMenu()
        {
            GameType gameType;
            int index;
            Console.Clear();
            while (true)
            {
                Console.WriteLine("\n\t What game rules do you want to play?\n");
                Console.WriteLine("\t - Press 1 for Yahtzee");
                Console.WriteLine("\t - Press 2 for Yatzy");

                string input = Console.ReadLine();
                if (Int32.TryParse(input, out index) && index >= 1 && index <= 2)
                {

                    if (index == 1)
                        gameType = GameType.Yahtzee;
                    else
                        gameType = GameType.Yatzy;
                    break;
                }
                else
                {
                    Console.Clear();
                    PrintErrorMessage("\t Invalid input");
                }
            }
            Welcome(gameType);
            return gameType;

        }

        public void Welcome(GameType gameType)
        {
            Console.Clear();
            Console.WriteLine(welcome + gameType.ToString());
            if (gameType == GameType.Yahtzee)
            {
                Console.WriteLine(rules + "https://en.wikipedia.org/wiki/Yahtzee \n\n");
            }
            else if (gameType == GameType.Yatzy)
            {
                Console.WriteLine(rules + "https://en.wikipedia.org/wiki/Yatzy \n\n");
            }
        }
    }
}
