using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Yahtzee.Model;

namespace Yahtzee.View
{
    class ScoreView : Display
    {
        public ScoreView()
        {
        }
        public void RenderRoundScore(int roundScore, Category usedCategory)
        {
            PrintMessage("Received " + roundScore + " points for category " + CategoryModel.GetName(usedCategory));
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
            Console.WriteLine("\n   YAHTZEE SCOREBOARD");
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
                foreach (Category category in CategoryModel.GetList())
                {
                    Console.Write("|" + category + "\t");
                    if (category <= Category.Sixes || category == Category.Chance)
                        Console.Write("\t");
                    foreach (Player player in players)
                    {
                        bool exist;
                        Console.Write(player.GetScore(category, out exist) + "\t");
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


