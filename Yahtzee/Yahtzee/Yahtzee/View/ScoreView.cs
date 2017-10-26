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
        public void RenderRoundScore(int roundScore, Categorie usedCategorie)
        {
            Console.WriteLine("Recieved " + roundScore + " points for categorie " + CategorieModel.GetName(usedCategorie));
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

            foreach (Categorie categorie in CategorieModel.GetList())
            {
                Console.Write("|" + categorie + "\t");
                if (categorie <= Categorie.Sixes || categorie == Categorie.Chance)
                    Console.Write("\t");
                foreach (Player player in players)
                {
                    Console.Write(player.Score.GetScoreInScoreCard(categorie) + "\t");
                }
                Console.WriteLine(" |\n" + divider);
            }
            Console.Write("|Sum\t\t");
            foreach (Player player in players)
            {
                Console.Write(player.Score.GetTotalScore() + "\t");
            }
            Console.WriteLine(" |\n^" + end+"^");
            Console.ResetColor();
        }
    }
}


