using Yahtzee.Model.Categories;

namespace Yahtzee.Model
{
    class Score
    {
        public Category.Type UsedCategory { get; private set; }

        public int Points { get; private set; }

        public Score(Category.Type category, int points)
        {
            UsedCategory = category;
            Points = points;
        }
    }
}
