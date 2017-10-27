using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Model;

namespace Yahtzee.View
{
    class ScoreView
    {
        public ScoreView()
        {
        }
        public void RenderRoundScore(int roundScore, Category usedCategorie)
        {
            Console.WriteLine("Recieved " + roundScore + " points for categorie " + CategoryModel.GetName(usedCategorie));
        }
        public void RenderScoreBoard(List<Player> players)
        {
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

            foreach (Category categorie in CategoryModel.GetList())
            {
                Console.Write("|" + categorie + "\t");
                if (categorie <= Category.Sixes || categorie == Category.Chance)
                    Console.Write("\t");
                foreach (Player player in players)
                {
                    bool exist;
                    Console.Write(player.GetScore(categorie, out exist) + "\t");
                }
                Console.WriteLine(" |\n" + divider);
            }
            Console.Write("|Sum\t\t");
            foreach (Player player in players)
            {
                Console.Write(player.GetTotalScore() + "\t");
            }
            Console.WriteLine(" |\n^" + end+"^");
            Console.ResetColor();
        }
    }
}


