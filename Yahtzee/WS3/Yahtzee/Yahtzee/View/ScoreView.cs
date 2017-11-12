using System;
using System.Collections.Generic;
using System.Linq;
using Yahtzee.Model;
using Yahtzee.Model.Categories;

namespace Yahtzee.View
{
    class ScoreView : Display
    {
        private Category category;
        public ScoreView(Category category)
        {
            this.category = category;
        }
        public void RenderRoundScore(int roundScore, Category.Type usedCategory)
        {
            PrintMessage("Received " + roundScore + " points for category " + category.GetName(usedCategory) + "\n");
        }
        public void RenderScoreBoard(List<Player> players, string date, bool fullList)
        {
            if (date != null)
                Console.WriteLine("\nGame played " + date);

            string divider = "|";
            string end = "";
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < players.Count() + 2; i++)
            {
                divider += "--------";
                end += "********";
            }
            divider += "|";
            Console.WriteLine("\n  SCOREBOARD");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("|"+end+"|");
            Console.Write("|\t\t");
            foreach (Player player in players)
            {
                Console.Write(player.Name + "\t");
            }
            Console.WriteLine(" |\n" + divider);

            if (fullList)
            {
                foreach (Category.Type cat in category.GetValues())
                {
                    string name = String.Format("|{0,-14}\t", category.GetName((int)cat));
                    Console.Write(name);
    //                Console.Write("|" + category + "\t");
                    foreach (Player player in players)
                    {
                        bool exist;
                        Console.Write(player.GetScore(cat, out exist) + "\t");
                    }
                    Console.WriteLine(" |\n" + divider);
                }
                Console.Write("|");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Sum\t\t");
            foreach (Player player in players)
            {
                Console.Write(player.GetTotalScore() + "\t");
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" |\n^" + end+"^");
            Console.ResetColor();
        }
    }
}


