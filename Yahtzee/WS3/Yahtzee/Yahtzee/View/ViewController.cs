using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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

        public void RenderRound(int roundNumber)
        {
            roundView.RenderRound(roundNumber);

        }
        public void RenderRound(string name)
        {
            roundView.RenderRound(name);
            
        }

        public bool[] GetDieToRoll()
        {
            return roundView.GetDieToRoll();
        }

        public void RenderDie(CollectionOfDice collectionOfDice)
        {
            roundView.RenderDie(collectionOfDice);
        }

        public Category RenderCategory()
        {
            return roundView.RenderCategory();
        }

        public void RenderDieToRoll(bool[] DieToRoll, string decision)
        {
            roundView.RenderDieToRoll(DieToRoll, decision);
        }

        public void RenderRoundScore(int roundScore, Category usedCategory)
        {
            scoreView.RenderRoundScore(roundScore, usedCategory);
        }

        public bool ContinueGame()
        {
            return roundView.ContinueGame();
        }
        public bool ResumeGame()
        {
            return roundView.SelectActivity("\nDo you want to resume a saved game (y/n)");
        }
        public bool ViewGameResult()
        {
            return roundView.SelectActivity("\nDo you want to inspect a saved game (y/n)");
        }
        public bool ViewFullList()
        {
            return roundView.SelectActivity("\nDo you want to view the full score board (y) or the short score borard (n) of the game (y/n)");
        }
        public void GameSaved(string fileName)
        {
            roundView.GameSaved(fileName);
        }

        public void RenderScoreBoard(List<Player> players, string date = null, bool fullList = true)
        {
            scoreView.RenderScoreBoard(players, date, fullList);
        }

        public string SelectGame(FileInfo[] files)
        {
            return roundView.SelectGame(files);
        }
    }
}
