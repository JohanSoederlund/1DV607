using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Model.Categories;

namespace Yahtzee.Model
{
    
    class Player
    {
        private List<Score> scoreList;
        public string Decision { get; protected set; }

        public Player()
        {
           
            this.scoreList = new List<Score>();
        }

        public Player(string name, bool robot = false): this()
        {
            Name = name;
            IsRobot = robot;
        }
        public Player(string name, List<Score> scores,  bool robot = false) : this(name, robot)
        {
            foreach (Score score in scores)
            {
                scoreList.Add(score);
            }
        }

        public string Name { get; private set; }
        public bool IsRobot { get; private set; }

        public void AddScore(Category.Type category, int point)
        {
            scoreList.Add(new Score(category, point));
        }

        public int GetScore(Category.Type category, out bool exist)
        {
            Score score = scoreList.Find(scoreObj => scoreObj.UsedCategory == category);

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
            Score[] scoreListCopy = new Score[scoreList.Count];

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
        public bool GetCategoryUsed(Category.Type category)
        {
            Score score = scoreList.Find(scoreObj => scoreObj.UsedCategory == category);
            if (score != null)
            {
                return true;
            }
            return false;
        }
        public List<Category.Type> GetUsedCategories(Category category)
        {
            List<Category.Type> unavaiableCategories = new List<Category.Type>();
            foreach (Category.Type cat in category.GetValues())
            {
                Score score = scoreList.Find(scoreObj => scoreObj.UsedCategory == cat);
                if (score != null)
                {
                    unavaiableCategories.Add(cat);
                }
            }
            return unavaiableCategories;
        }
    }
}
