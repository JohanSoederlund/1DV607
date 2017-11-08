using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Yahtzee.Model;
using Yahtzee.View;

namespace Yahtzee.Controller
{
    class Game : GameModel
    {
        private DataBase dataBase;
        private List<Player> players;
        private Rules rules;
        private CollectionOfDice collectionOfDice;

        private ViewController viewController;

        public Game()
        {
            InitGame();
            RunGame();
        }
        
        private bool[] DieToRoll { get; set; }

        private void InitGame()
        {
            dataBase = new DataBase();
            collectionOfDice = new CollectionOfDice();
            rules = new Rules(collectionOfDice);
            viewController = new ViewController();

            string resumeGameFile = "";
            string viewGameFile = "";
            if (viewController.ViewGameResult())
            {
                FileInfo[] files = dataBase.ListAllGames();
                viewGameFile = viewController.SelectGame(files);
            }
            if (viewGameFile != "")
            {
                bool fullList = viewController.ViewFullList();
                DateTime date = new DateTime();
                int roundNumber = 0;
                players = dataBase.GetFromFile(rules, viewGameFile, out date, out roundNumber);
                Date = date;
                RoundNumber = roundNumber;
                viewController.RenderScoreBoard(players, date.ToString(), fullList);
            }

            if (viewController.ResumeGame())
            {
                FileInfo[] files = dataBase.ListAllGames();
                resumeGameFile = viewController.SelectGame(files);
            }
            if (resumeGameFile != "")
            {
                DateTime date = new DateTime();
                int roundNumber = 0;
                players = dataBase.GetFromFile(rules, resumeGameFile, out date, out roundNumber);
                Date = date;
                RoundNumber = roundNumber;
            }
            else
            {
                PlayerSetup();
            }
        }
        private void RunGame()
        {
            string fileName = "";
            int startRound = RoundNumber+1;
            for (int i = startRound; i <= CategoryModel.GetSize(); i++)
            {
                if (i != startRound && !viewController.ContinueGame())
                {
                    fileName = dataBase.SaveToFile(Date, RoundNumber, players);
                    viewController.GameSaved(fileName);
                    return;
                }
                RunRound(i);
                IncrementRoundNumber();
            }
            //todo: this instead of players 
            fileName = dataBase.SaveToFile(Date, RoundNumber, players);
            viewController.GameSaved(fileName);
        }

        private void PlayerSetup()
        {
            bool robot;
            players = new List<Player>();
            int numberOfPlayers = viewController.NumberOfPlayers();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                string name = viewController.PlayerName(out robot);
                if (robot)
                {
                    players.Add(new Robot(GetNumberOfRobots()+1, rules));

                } else
                {
                    players.Add(new Player(name, rules));
                }
                
            }
        }

        private int GetNumberOfRobots()
        {
            int numberOfRobots = 0;
            foreach(Player player in players)
            {
                if (player.IsRobot)
                {
                    numberOfRobots++;
                }
            }
            return numberOfRobots;
        }

        private void RunRound(int roundNumber)
        {
            viewController.RenderRound(roundNumber);
            foreach (Player player in players)
            {
                DieToRoll = new bool[] { true, true, true, true, true };
                if (player.IsRobot)
                {
                    RobotRound((Robot)player, roundNumber);
                } else
                {
                    PlayerRound(player, roundNumber);
                }

            }
            viewController.RenderScoreBoard(players);
        }

        private void RobotRound(Robot robot, int roundNumber)
        {
            viewController.RenderRound(robot.Name);
            // Starting roll
            collectionOfDice.Roll(DieToRoll);
            viewController.RenderDie(collectionOfDice);
            DieToRoll = robot.DecideDiceToRoll(collectionOfDice.GetNumberOfDiceFaceValue(), collectionOfDice.GetDie());
            viewController.RenderDieToRoll(DieToRoll, robot.Decision);
            Thread.Sleep(3000);

            // First reroll
            if (AnyDiceToRoll())
            {
                collectionOfDice.Roll(DieToRoll);
                viewController.RenderDie(collectionOfDice);
                DieToRoll = robot.DecideDiceToRoll(collectionOfDice.GetNumberOfDiceFaceValue(), collectionOfDice.GetDie());
                viewController.RenderDieToRoll(DieToRoll, robot.Decision);
                Thread.Sleep(3000);

                // Second reroll
                if (AnyDiceToRoll())
                {
                    collectionOfDice.Roll(DieToRoll);
                    viewController.RenderDie(collectionOfDice);
                }
            }

            Category usedCategory = robot.CalcBestValueAndAddToScorelist();
            bool exist = false;
            int roundScore = robot.GetScore(usedCategory, out exist);
            if (exist)
            {
                viewController.RenderRoundScore(roundScore, usedCategory);
            }
            Thread.Sleep(1000);
        }

        private void PlayerRound(Player player, int roundNumber)
        {
            viewController.RenderRound(player.Name);
            collectionOfDice.Roll(DieToRoll);

            viewController.RenderDie(collectionOfDice);
            DieToRoll = viewController.GetDieToRoll();

            if (AnyDiceToRoll())
            {
                collectionOfDice.Roll(DieToRoll);

                viewController.RenderDie(collectionOfDice);
                DieToRoll = viewController.GetDieToRoll();
                if (AnyDiceToRoll())
                {
                    collectionOfDice.Roll(DieToRoll);
                    viewController.RenderDie(collectionOfDice);
                }
            }
            bool categoryUpdated = false;
            while (!categoryUpdated)
            {
                Category categoryToUse = viewController.RenderCategory();
                if (!player.GetCategoryUsed(categoryToUse))
                {
                    player.AddScore(categoryToUse);
                    categoryUpdated = true;
                    bool exist = false; 
                    int roundScore = player.GetScore(categoryToUse, out exist);
                    viewController.RenderRoundScore(roundScore, categoryToUse);
                }
            }
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

        private int GetRoundsPlayed()
        {
            return RoundNumber;
        }

    }



    //dataBase.SaveToFile(players);
    //players = dataBase.GetFromFile();

}


