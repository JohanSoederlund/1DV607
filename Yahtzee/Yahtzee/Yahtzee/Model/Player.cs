using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class Player
    {
        private List<Score> scoreList;
        public Player()
        {
            scoreList = new List<Score>();
        }

        public Player(string name, bool robot = false) : this()
        {
            Name = name;
            IsRobot = robot;
        }
        public Player(string name, List<Score> scores, bool robot = false) : this(name, robot)
        {
            foreach (Score score in scores)
            {
                scoreList.Add(score);
            }
        }


        public string Name { get; set; }
        public bool IsRobot { get; private set; }

        public void AddScore(Categorie categorie, int points)
        {
            scoreList.Add(new Score(categorie, points));
        }

        public int GetScore(Categorie categorie, out bool exist)
        {
            Score score = scoreList.Find(scoreObj => scoreObj.UsedCategorie == categorie);

            if (score != null)
            {
                exist = true;
                return score.Points;
            }
            else
            {
                exist = false;
                return 0;
            }
        }
        public Score[] GetScoreList()
        {

            Score[] scoreListCopy = new Score[GetScoreSize()];

            scoreList.CopyTo(scoreListCopy, 0);
            return scoreListCopy;
        }
        public int GetTotalScore()
        {
            int sum = 0;
            foreach(Score score in scoreList)
            {
                sum += score.Points;
            }
            return sum;
        }
        public bool GetCategorieUsed(Categorie categorie)
        {
            Score score = scoreList.Find(scoreObj => scoreObj.UsedCategorie == categorie);
            if (score != null)
            {
                return true;
            }
            return false;
        }


        public int GetScoreSize()
        {
            return scoreList.Count;
        }
    }
}
