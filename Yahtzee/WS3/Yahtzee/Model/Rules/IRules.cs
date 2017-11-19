using Yahtzee.Model.Categories;

namespace Yahtzee.Model.Rules
{
    interface IRules
    {
        int GetValueForCategory(Category.Type category);

        bool HaveLargeStraight();

        bool HaveSmallStraight();

        BaseRules BaseRules { get; set; }
    }
}
