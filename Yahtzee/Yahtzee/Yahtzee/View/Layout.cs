using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Yahtzee.Model;

namespace Yahtzee.View
{
    class Layout
    {
        private ScoreView scoreView;
        private SetupView setupView;
        private RoundView roundView;

        private readonly string mainMenu = "Welcome to Yahtzee! \nThe rules are ... \n";
        public Layout()
        {
            scoreView = new ScoreView();
            setupView = new SetupView();
            roundView = new RoundView();
            Restart = false;
            Console.WriteLine(mainMenu);
        }

        public bool Restart { get; private set; }

        public int NumberOfPlayers()
        {
            return setupView.NumberOfPlayers();
        }

        public string PlayerName(out bool robot)
        {
            robot = setupView.IsRobot();
            if (robot)
            {
                return "";
            }
            return setupView.PlayerName();
        }

        public void RenderRound(string name, int roundNumber)
        {
            roundView.RenderRound(name, roundNumber);
            
        }

        public bool[] GetDieToRoll()
        {
            return roundView.GetDieToRoll();
        }

        public void RenderDie(CollectionOfDice collectionOfDice)
        {
            roundView.RenderDie(collectionOfDice);
        }

        public Categorie RenderCategorie(bool[] usedCategories)
        {
            return roundView.RenderCategorie(usedCategories);
        }

        public void RenderDieToRoll(bool[] DieToRoll, string decision)
        {
            roundView.RenderDieToRoll(DieToRoll, decision);
        }

        public void RenderRoundScore(int roundScore, int usedCategorie)
        {
            roundView.RenderRoundScore(roundScore, usedCategorie);
        }

        public void RenderScoreBoard(List<Player> players)
        {
            scoreView.RenderScoreBoard(players);
        }

        public bool ContinueGame()
        {
            return roundView.ContinueGame();
        }
        public bool ResumeGame()
        {
            return roundView.ResumeGame();
        }
    }
}
