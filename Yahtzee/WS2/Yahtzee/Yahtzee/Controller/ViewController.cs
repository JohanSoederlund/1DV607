﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
           }

        public int NumberOfPlayers()
        {
            return setupView.NumberOfPlayers();
        }

        public string PlayerName(int number, out bool robot)
        {
            string name = "";

            robot = setupView.IsRobot(number);
            if (!robot)
            {
                name = setupView.PlayerName(number);
            }
            Console.Clear();
            return name;
        }

        public void RenderRoundNumber(int roundNumber)
        {
            roundView.RenderRoundNumber(roundNumber);
        }
        public void RenderRound(string name)
        {
            roundView.RenderRound(name);
           
        }
        public bool[] GetDieToRoll()
        {
            return roundView.GetDieToRoll();
        }
        public void RenderDie(int[] die)
        {
            roundView.RenderDie(die);
        }
        public void RenderUnavailableCategories(List<Category> unavailableCategories)
        {
            if (roundView.SelectActivity(DisplayType.ViewAvaialbleCategories, false))
            {
                roundView.RenderUnavailableCategories(unavailableCategories);
            }
        }
        public Category RenderCategory(List<Category> unavailableCategories)
        {
            return roundView.RenderCategory(unavailableCategories);
        }
        public void RenderDieToRoll(bool[] DieToRoll, string decision = "")
        {
            roundView.RenderDieToRoll(DieToRoll, decision);
            Thread.Sleep(2000);
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
            return roundView.SelectActivity(DisplayType.ResumeSavedGame);
        }
        public bool ViewGameResult()
        {
            return roundView.SelectActivity(DisplayType.InspectSavedGame);
        }
        public bool ViewFullList()
        {
            return roundView.SelectActivity(DisplayType.ViewFullScoreBord);
        }
        public void GameSaved(string fileName)
        {
            roundView.GameSaved(fileName);
        }
        public void GameFinished(string winner, int score)
        {
            roundView.GameFinished(winner, score);
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
