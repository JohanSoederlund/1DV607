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
        private readonly string divider = "\n----------------------------------------------------------------------";
        public ScoreView()
        {
        }
        public void RenderScoreBoard(List<Player> players)
        {
            Console.Write("\t\t");
            foreach (Player player in players)
            {
                Console.Write(player.Name + "\t");
            }
            Console.WriteLine(divider);
            for (int i = 0; i < Enum.GetNames(typeof(Categorie)).Length; i++)
            {
                Console.Write(Enum.GetName(typeof(Categorie), i) + "\t");
                if (i <= 5 || i >= 11)
                    Console.Write("\t");
                foreach (Player player in players)
                {
                    Console.Write(player.Score.ScoreCard[i] + "\t");
                }
                Console.WriteLine(divider);
            }
        }
    }
}
