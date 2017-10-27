using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            IncrementRoundNumber();
            RunGame();
        }

        private bool[] DieToRoll { get; set; }

        private void InitGame()
        {
            dataBase = new DataBase();
            collectionOfDice = new CollectionOfDice();
            rules = new Rules(collectionOfDice);
            viewController = new ViewController();

            if (viewController.ResumeGame())
            {
                players = dataBase.GetFromFile(rules);
                //RoundNumber = dataBase.GetRoundNumberFromFile();
                //Date = dataBase.GetDateFromFile();
            }
            else
            {
                PlayerSetup();
            }
        }
        private void RunGame()
        {
            for (int i = RoundNumber; i <= CategoryModel.GetSize(); i++)
            {
                if (i != RoundNumber && !viewController.ContinueGame())
                {
                    dataBase.SaveToFile(players);
                    return;
                }
                RunRound(i);
                Thread.Sleep(2000);
            }
            //todo: this instead of players 
            dataBase.SaveToFile(players);
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
            viewController.RenderRound(robot.Name, roundNumber);
            collectionOfDice.Roll(DieToRoll);

            viewController.RenderDie(collectionOfDice);
            Thread.Sleep(1000);
            DieToRoll = robot.DecideDiceToRoll(collectionOfDice.GetNumberOfDiceFaceValue(), collectionOfDice.GetDie());
            viewController.RenderDieToRoll(DieToRoll, robot.Decision);

            if (AnyDiceToRoll())
            {
                Thread.Sleep(1000);
                collectionOfDice.Roll(DieToRoll);

                viewController.RenderDie(collectionOfDice);
                Thread.Sleep(1000);
                DieToRoll = robot.DecideDiceToRoll(collectionOfDice.GetNumberOfDiceFaceValue(), collectionOfDice.GetDie());
                viewController.RenderDieToRoll(DieToRoll, robot.Decision);
                if (AnyDiceToRoll())
                {
                    Thread.Sleep(1000);
                    collectionOfDice.Roll(DieToRoll);
                    viewController.RenderDie(collectionOfDice);
                }
            }

            Category usedCategorie = robot.CalcBestValue();
            bool exist = false;
            int roundScore = robot.GetScore(usedCategorie, out exist);
            if (exist)
            {
                viewController.RenderRoundScore(roundScore, usedCategorie);
            }
            Thread.Sleep(1000);
        }

        private void PlayerRound(Player player, int roundNumber)
        {
            viewController.RenderRound(player.Name, roundNumber);
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
                Category categoryToUse = viewController.RenderCategorie();
                if (!player.GetCategorieUsed(categoryToUse))
                {
                    player.AddScore(categoryToUse);
                    categoryUpdated = true;
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


