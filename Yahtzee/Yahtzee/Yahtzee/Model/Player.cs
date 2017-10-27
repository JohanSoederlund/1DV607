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
        protected Rules rules;
        public Player(Rules rules)
        {
            this.rules = rules;
            this.scoreList = new List<Score>();
        }

        public Player(string name, Rules rules, bool robot = false) : this(rules)
        {
            Name = name;
            IsRobot = robot;
        }
        public Player(string name, List<Score> scores, Rules rules, bool robot = false) : this(name, rules, robot)
        {
            foreach (Score score in scores)
            {
                scoreList.Add(score);
            }
        }


        public string Name { get; set; }
        public bool IsRobot { get; private set; }

        public void AddScore(Category categorie)
        {
            scoreList.Add(new Score(categorie, rules.GetValueForCategorie(categorie)));
        }

        public int GetScore(Category categorie, out bool exist)
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
        public bool GetCategorieUsed(Category categorie)
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
