using System;
using System.Collections.Generic;
using System.IO;
using Yahtzee.Model;
using Yahtzee.View;
using Yahtzee.Model.Rules;
using Yahtzee.Model.Categories;

namespace Yahtzee.Controller
{
    class Game
    {
        private DataBase dataBase;
        private List<Player> players;
        private Factory factory;
        private IRules rules;
        private CollectionOfDice collectionOfDice;
        private Category category;
        private ViewController viewController;
        private GameType gameType;

        private DateTime Date { get; set; }
        private int RoundNumber { get; set; }

        public Game()
        {
            InitGame();
            RunGame();
        }
        
        private bool[] DieToRoll { get; set; }

        private void InitGame()
        {
            gameType = new MainMenu().RenderStartMenu();

            collectionOfDice = new CollectionOfDice();
            factory = new Factory(gameType);
            rules = factory.GetRules(collectionOfDice);
            category = factory.GetCategory();
            dataBase = new DataBase(category, rules, gameType);
            viewController = new ViewController(category, collectionOfDice);

            while (viewController.ViewGameResult())
            {
                FileInfo[] files = dataBase.ListAllGames();
                string viewGameFile = viewController.SelectGame(files);
                if (viewGameFile != "")
                {
                    ViewGameFile(viewGameFile);
                }
            }
            if (viewController.ResumeGame())
            {
                FileInfo[] files = dataBase.ListAllGames();
                string resumeGameFile = viewController.SelectGame(files);
                if (resumeGameFile != "")
                {
                    ResumeGameFile(resumeGameFile);
                }
            }
            else
            {
                Date = DateTime.Now;
                RoundNumber = 0;
                PlayerSetup();
            }
        }

        private void ViewGameFile(string viewGameFile)
        {
            List<string> items = new List<string>();
            bool fullList = viewController.ViewFullList();
            DateTime date = new DateTime();
            int roundNumber = 0;
            players = dataBase.GetFromFile(rules, viewGameFile, out date, out roundNumber);

            Date = date;
            RoundNumber = roundNumber;
            viewController.RenderScoreBoard(players, date.ToString(), fullList);
        }

        private void ResumeGameFile(string resumeGameFile)
        {
            DateTime date = new DateTime();
            int roundNumber = 0;
            players = dataBase.GetFromFile(rules, resumeGameFile, out date, out roundNumber);
            Date = date;
            RoundNumber = roundNumber;
        }

        private void PlayerSetup()
        {
            bool robot;
            players = new List<Player>();
            int numberOfPlayers = viewController.NumberOfPlayers();
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                string name = viewController.PlayerName(i, out robot);
                if (robot)
                {
                    players.Add(new Robot(GetNumberOfRobots() + 1, rules, category, gameType));
                }
                else
                {
                    players.Add(new Player(name));
                }
            }
        }

        private void RunGame()
        {
            string fileName = "";
            int startRound = RoundNumber+1;
            for (int i = startRound; i <= category.Length(); i++)
            {
                if (i != startRound && !viewController.ContinueGame())
                {
                    fileName = dataBase.SaveToFile(Date, RoundNumber, players);
                    viewController.GameSaved(fileName);
                    return;
                }
                RunRound(i);
                RoundNumber++;
            }
            EndGame();
        }

        private void RunRound(int roundNumber)
        {
            viewController.RenderRoundNumber(roundNumber);
            foreach (Player player in players)
            {
                DieToRoll = new bool[] { true, true, true, true, true };
                PlayRound(player);
            }
            viewController.RenderScoreBoard(players);
        }
        private void PlayRound(Player player)
        {
            Robot robot = player as Robot;
            Category.Type categoryToUse = category.Chance();

            viewController.RenderRound(player.Name);
            for (int rollNumber = 1; rollNumber <= 3; rollNumber++)
            {
                if (AnyDiceToRoll())
                {
                    collectionOfDice.Roll(DieToRoll);
                    if (rollNumber < 3)
                    {
                        if (player.IsRobot)
                        {
                            DieToRoll = robot.DecideDiceToRoll(collectionOfDice.GetNumberOfDiceFaceValue(), collectionOfDice.GetDie());
                        }
                        else
                        {
                            if (rollNumber == 1)
                                viewController.RenderUnavailableCategories(player.GetUsedCategories(category));
                            DieToRoll = viewController.GetDieToRoll();
                        }
                        viewController.RenderDieToRoll(DieToRoll, player.Decision);
                    }
                }
            }
            if (player.IsRobot)
            {
                categoryToUse = robot.SelectCategoryToUse();
            }
            else
            {
                categoryToUse = viewController.RenderCategory(player.GetUsedCategories(category));
            }
            player.AddScore(categoryToUse, rules.GetValueForCategory(categoryToUse));

            bool exist = false;
            int roundScore = player.GetScore(categoryToUse, out exist);
            if (exist)
            {
                viewController.RenderRoundScore(roundScore, categoryToUse);
            }
        }

        private void EndGame()
        {
            string fileName = dataBase.SaveToFile(Date, RoundNumber, players);
            int highScore = 0;
            string winner = "";
            foreach (Player player in players)
            {
                if (player.GetTotalScore() == highScore)
                {
                    winner = "We have a draw";
                    highScore = player.GetTotalScore();
                }
                if (player.GetTotalScore() > highScore)
                {
                    winner = player.Name;
                    highScore = player.GetTotalScore();
                }
            }
            viewController.GameFinished(winner, highScore);
            viewController.GameSaved(fileName);
        }

        private bool AnyDiceToRoll()
        {
            bool roll = false;
            for (int i=0; i< DieToRoll.Length;i++)
            {
                if (DieToRoll[i])
                    roll = true;
            }
            return roll;
        }

        private int GetNumberOfRobots()
        {
            int numberOfRobots = 0;
            foreach (Player player in players)
            {
                if (player.IsRobot)
                {
                    numberOfRobots++;
                }
            }
            return numberOfRobots;
        }
    }
}


