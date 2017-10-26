using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Yahtzee.Controller;

namespace Yahtzee.Model
{
    class DataBase
    {
        private readonly string pathToDB = $"{Environment.CurrentDirectory.Substring(0, (Environment.CurrentDirectory.Length - 9))}DB\\Players.txt";
        private JavaScriptSerializer serializer;
        public DataBase()
        {
            serializer = new JavaScriptSerializer();
        }

        public Object DataBaseObject { get; private set; }

        public void SaveToFile(List<Player> players)
        {
            StreamWriter file = new StreamWriter(pathToDB);
            foreach (Player player in players)
            {
                string scoreCardToSave = serializer.Serialize(player.Score.ScoreCard);
                string usedCategories = serializer.Serialize(player.Score.UsedCategories);
                file.WriteLine(player.Name);
                file.WriteLine(serializer.Serialize(player.IsRobot));
                file.WriteLine(scoreCardToSave);
                file.WriteLine(usedCategories);
            }
            file.Close();
        }

        public List<Player> GetFromFile(Rules rules)
        {
            string line;
            List<Player> players = new List<Player>();
            List<string> items = new List<string>();
            StreamReader file = new StreamReader(pathToDB);
            while((line = file.ReadLine()) != null)
            {
                items.Add(line);
            }
            file.Close();
            for (int i = 0; i < (items.Count / 4);i++)
            {
                int[] scoreCardToSave = serializer.Deserialize<int[]>(items[i*3 + 2]);
                bool[] usedCategories = serializer.Deserialize<bool[]>(items[i*3 + 3]);
                bool isRobot = serializer.Deserialize<bool>(items[i * 3 + 1]);
                if (isRobot)
                {
                    Robot robot = new Robot(items[i * 3], rules, scoreCardToSave, usedCategories);
                    players.Add(robot);
                }
                else
                {
                    Player player = new Player(items[i * 3], scoreCardToSave, usedCategories);
                    players.Add(player);
                }
            }
            
            return players;
        }

    }
}
