using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Yahtzee.Model;

namespace Yahtzee.View
{
    class ViewController
    {
        private ScoreView scoreView;
        private SetupView setupView;
        private RoundView roundView;

        
        public ViewController()
        {
            setupView = new SetupView();
            scoreView = new ScoreView();
            roundView = new RoundView();
            Restart = false;
            
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

        public Category RenderCategorie()
        {
            return roundView.RenderCategorie();
        }

        public void RenderDieToRoll(bool[] DieToRoll, string decision)
        {
            roundView.RenderDieToRoll(DieToRoll, decision);
        }

        public void RenderRoundScore(int roundScore, Category usedCategorie)
        {
            scoreView.RenderRoundScore(roundScore, usedCategorie);
        }

        public bool ContinueGame()
        {
            return roundView.ContinueGame();
        }
        public bool ResumeGame()
        {
            return roundView.ResumeGame();
        }


        public void RenderScoreBoard(List<Player> players)
        {
            scoreView.RenderScoreBoard(players);
        }
    }
}
