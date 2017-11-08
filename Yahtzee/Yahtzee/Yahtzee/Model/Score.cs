using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class Score
    {
        public Category UsedCategory { get; private set; }

        public int Points { get; private set; }

        public Score()
        {
        }

        public Score(Category category, int points)
        {
            UsedCategory = category;
            Points = points;
        }
    }
}
