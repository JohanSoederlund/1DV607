using System;
using System.Collections.Generic;
using System.IO;
using Yahtzee.Model.Categories;
using Yahtzee.Model.Rules;

namespace Yahtzee.Model
{
    class DataBase
    {
        private readonly string pathToDB = $"{Environment.CurrentDirectory}\\Database\\";
        private string fileName = "";

        private IRules rules;
        private GameType gameType;
        private Category category;

        public DataBase(Category category, IRules rules, GameType gameType)
        {
            this.category = category;
            this.gameType = gameType;
            this.rules = rules;
            this.fileName = gameType.ToString();
            Directory.CreateDirectory(pathToDB);
        }
        public string SaveToFile(DateTime date, int roundNumber, List<Player>players) 
        {

            string dateStr = date.ToString();
            dateStr = dateStr.Substring(2, 2) + dateStr.Substring(5, 2) + dateStr.Substring(8, 2) + dateStr.Substring(11, 2) + dateStr.Substring(14, 2) + dateStr.Substring(17, 2) + ".txt";

            StreamWriter file = new StreamWriter(pathToDB+fileName+dateStr);

            string output = date.ToString();
            output = date.ToString();
            file.WriteLine(output);
            output = roundNumber.ToString();
            file.WriteLine(output);
            foreach (Player player in players)
            {
                output = player.Name;
                file.WriteLine(output);
                output = player.IsRobot.ToString().ToLower();
                file.WriteLine(output);
                Score[] score = player.GetScoreList();
                for (int i=0; i < score.Length; i++)
                {
                    output = "";
                    output += score[i].Points + "|" + score[i].UsedCategory;
                    file.WriteLine(output);
                }
            }
            file.Close();
            return pathToDB+fileName+ dateStr;
        }

        public List<Player> GetFromFile(IRules rules, string fileName, out DateTime date, out int roundNumber)
        {
            string line;
            List<Player> players = new List<Player>();
            List<string> items = new List<string>();
            StreamReader file = new StreamReader(pathToDB+fileName);

            while((line = file.ReadLine()) != null)
            {
                items.Add(line);
            }
            file.Close();

            date = Convert.ToDateTime(items[0]);
            roundNumber = int.Parse(items[1]);

            string name = "";
            bool isRobot = false;
            int rowsForPlayer = roundNumber + 2;
            int noOfPlayers = (items.Count - 2) / (roundNumber + 2);
            int indexStartPlayer = 2;
            for (int i = 0; i < noOfPlayers ;i++)
            {
                indexStartPlayer = 2 + i * rowsForPlayer;
                List<Score> scoreList = new List<Score>();
                name = items[indexStartPlayer];
                isRobot = bool.Parse(items[indexStartPlayer + 1]);
                string[] scoreItems;

                for (int j = 0; j < roundNumber; j++)
                {
                    scoreItems = items[indexStartPlayer + 2 + j].Split('|');
                    int point = Int32.Parse(scoreItems[0]);
                    Category.Type cat = (Category.Type)Enum.Parse(typeof(Category.Type), (scoreItems[1]));
                    Score score = new Score(cat, point);
                    scoreList.Add(score);
                }
                if (isRobot)
                {
                    Robot robot = new Robot(name, rules, category, scoreList, gameType);
                    players.Add(robot);
                }
                else
                {
                    Player player = new Player(name, scoreList);
                    players.Add(player);
                }
            }
            return players;
        }

        public FileInfo[] ListAllGames()
        {
            DirectoryInfo d = new DirectoryInfo(pathToDB);//Assuming Test is your Folder
            FileInfo[] files = d.GetFiles("*"+gameType.ToString()+"*.txt"); //Getting Text files
            return files;
        }
    }
}
