using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Model.Rules;
using Yahtzee.Model.Categories;

namespace Yahtzee.Model
{
    class Robot : Player
    {
        private Category category;
        private IRules rules;
        public Robot(int id, IRules rules, Category category)
            : base("Robot" + id, true)
        {
            this.rules = rules;
            this.category = category;
        }
        public Robot(string name, IRules rules, Category category, List<Score> scores)
            : base(name, scores, true)
        {
            this.rules = rules;
            this.category = category;
        }

        private bool[] Dice2Roll { get; set; }

        public bool[] DecideDiceToRoll(int[] diceVal, int[] die)
        {
            // This is the core of the robot strategy
            Dice2Roll = new bool[] { false, false, false, false, false };

            // Priority order for robot how to act on rolled die
            if (Stand()) ;
            else if (KeepThreeOrFourOfAKind(diceVal, die)) ;
            else if (KeepStraightChance(diceVal, die)) ;
            else if (KeepTwoPair(diceVal, die)) ;
            else if (KeepPair(diceVal, die)) ;
            else
            {
                Dice2Roll = new bool[] { true, true, true, true, true };
                Decision = "ROLL THEM ALL";
            }
            return Dice2Roll;
        }
        public Category.Type SelectCategoryToUse()
        {
            //intentially fall through all options to find best value
            int highestValue = 0;
            Category.Type highCategory = 0;
            int[] getValueForCategories = new int[category.Length()];

            foreach (Category.Type cat in category.GetValues())
            {


                int i = category.GetValue(cat);
                getValueForCategories[i] = rules.GetValueForCategory(cat);
                // always chose the highest score. If many on same vale chose highest category
                if (cat != category.Chance() && !GetCategoryUsed(cat) && getValueForCategories[i] >= highestValue)
                {
                    highestValue = getValueForCategories[i];
                    highCategory = cat;
                }
            }
            // Special rule when to chose category chance
            getValueForCategories[12] = rules.GetValueForCategory(category.Chance());
            if (!GetCategoryUsed(category.Chance()) && getValueForCategories[12] > highestValue && highestValue < 10 && highCategory > category.Threes() && highCategory < category.Yahtzee())  // Only chance if nothing better or equal
            {
                highCategory = category.Chance();
            }
            return highCategory;
        }


        private bool Stand()
        {
            if ((rules.BaseRules.HaveYahtzee()) && !GetCategoryUsed(category.Yahtzee()) ||
                (rules.BaseRules.HaveFullHouse()) && !GetCategoryUsed(category.FullHouse()) ||
                (rules.HaveLargeStraight()) && !GetCategoryUsed(category.LargeStraight()) ||
                (rules.HaveSmallStraight()) && !GetCategoryUsed(category.SmallStraight()))
            {
                Decision = "Stand";
                return true;
            }
            return false;
        }

        private bool KeepThreeOrFourOfAKind(int[] diceVal, int[] die)
        {
            for (int i = 0; i < diceVal.Length; i++)
            {
                if ((diceVal[i] == 4) || (diceVal[i] == 3))
                {
                    for (int j = 0; j < die.Length; j++)
                    {
                        if (die[j] != i + 1)
                            Dice2Roll[j] = true;
                    }
                    Decision = "KEEP THREE OR FOUR OF A KIND";
                    return true;
                }
            }
            return false;
        }

        private bool KeepStraightChance(int[] diceVal, int[] die)
        {
            // Keep good chance for straight, check small straight and large straight aren't taken
            // Keep three in a row but not down to dice value 1, i.e. only high or open straight
            if (!(GetCategoryUsed(category.SmallStraight()) && GetCategoryUsed(category.LargeStraight())))
            {
                for (int i = 5; i > 2; i--)
                {
                    if ((diceVal[i] > 0) && (diceVal[i - 1] > 0) && (diceVal[i - 2] > 0))
                    {
                        // easier to assume to roll each dice and select which ones not to in this case
                        Dice2Roll = new bool[] { true, true, true, true, true };
                        for (int j = 0; j < die.Length; j++)
                        {
                            if (die[j] == i + 1)
                            {
                                Dice2Roll[j] = false;
                                for (int k = 0; k < die.Length; k++)
                                {
                                    if (die[k] == i)
                                    {
                                        Dice2Roll[k] = false;
                                        for (int m = 0; m < die.Length; m++)
                                        {
                                            if (die[m] == i - 1)
                                            {
                                                Dice2Roll[m] = false;
                                                Decision = "KEEP GOOD CHANCE FOR STRAIGHT";
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            return false;
        }

        private bool KeepTwoPair(int[] diceVal, int[] die)
        {
            // Keep two pair for a full house, check full house isn't taken
            if (!GetCategoryUsed(category.FullHouse()))
            {
                int firstPairValue = 0;
                int secondPairValue = 0;
                for (int i = 0; i < diceVal.Length; i++)
                {
                    if (diceVal[i] == 2)
                    {
                        firstPairValue = i+1;
                        for (int j = 0; j < diceVal.Length; j++)
                        {
                            if ((diceVal[j] == 2) && (j+1 != firstPairValue))
                            {
                                for (int k = 0; k < die.Length; k++)
                                {
                                    secondPairValue = j+1;
                                    if ((die[k] != firstPairValue) && (die[k] != secondPairValue))
                                    {
                                        Dice2Roll[k] = true;
                                        Decision = "KEEP TWO PAIR FOR CHANCE TO FULL HOUSE";
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool KeepPair(int[] diceVal, int[] die)
        {
            // Keep pair if higher then 2
            for (int i = 2; i < diceVal.Length; i++)
            {
                if (diceVal[i] == 2)
                {
                    for (int j = 0; j < die.Length; j++)
                    {
                        if (die[j] != i + 1)
                            Dice2Roll[j] = true;
                    }
                    Decision = "KEEP PAIR";
                    return true;
                }
            }
            return false;
        }
    }
}
