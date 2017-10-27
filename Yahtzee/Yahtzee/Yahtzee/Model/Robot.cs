using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class Robot : Player
    {
        public Robot(int id, Rules rules)
            : base("Robot" + id, rules, true)
        {
        }
        public Robot(string name, Rules rules, List<Score> scores)
            : base(name, scores, rules, true)
        {
        }
        public string Decision { get; private set; }
        private bool[] Dice2Roll { get; set; }

        public bool[] DecideDiceToRoll(int[] diceVal, int[] die)
        {
            Dice2Roll = new bool[] { false, false, false, false, false };

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



        public Category CalcBestValue()
        {
            //intentially fall through all options to find best value
            int highestValue = 0;
            Category highCategory = 0;
            int[] getValueForCategories = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


            foreach (Category category in CategoryModel.GetList())
            {
                int i = (int)category;
                getValueForCategories[i] = rules.GetValueForCategorie(category);
                if (category != Category.Chance && !GetCategorieUsed(category) && getValueForCategories[i] >= highestValue)  // always chose the highest score and if many on same highest cat
                {
                    highestValue = getValueForCategories[i];
                    highCategory = category;
                }
            }

            getValueForCategories[12] = rules.GetValueForCategorie(Category.Chance);
            if (!GetCategorieUsed(Category.Chance) && getValueForCategories[12] > highestValue && highestValue < 10 && highCategory > Category.Threes && highCategory < Category.Yahtzee)  // Only chance if nothing better or equal
            {
                highCategory = Category.Chance;
            }

            AddScore(highCategory);
            return highCategory;
        }

        private bool Stand()
        {
            if ((rules.GetValueForCategorie(Category.Yahtzee) > 0) && !GetCategorieUsed(Category.Yahtzee) ||
                (rules.GetValueForCategorie(Category.FullHouse) > 0) && !GetCategorieUsed(Category.FullHouse) ||
                (rules.GetValueForCategorie(Category.LargeStraight) > 0) && !GetCategorieUsed(Category.LargeStraight) ||
                (rules.GetValueForCategorie(Category.SmallStraight) > 0) && !GetCategorieUsed(Category.SmallStraight))
            {
                Decision = "Stand";
                return true;   // stand
            }
            return false;
        }

        private bool KeepThreeOrFourOfAKind(int[] diceVal, int[] die)
        {
            for (int i = 0; i < 6; i++)
            {
                if ((diceVal[i] == 4) || (diceVal[i] == 3))
                {
                    for (int j = 0; j < 5; j++)
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
            if (!(GetCategorieUsed(Category.SmallStraight) && GetCategorieUsed(Category.LargeStraight)))
            {
                for (int i = 5; i > 2; i--)
                {
                    if ((diceVal[i] > 0) && (diceVal[i - 1] > 0) && (diceVal[i - 2] > 0))
                    {
                        // easier to assume to roll each dice and select which ones not to in this case
                        Dice2Roll = new bool[] { true, true, true, true, true };
                        for (int j = 0; j < 5; j++)
                        {
                            if (die[j] == i + 1)
                            {
                                Dice2Roll[j] = false;
                                for (int k = 0; k < 5; k++)
                                {
                                    if (die[k] == i)
                                    {
                                        Dice2Roll[k] = false;
                                        for (int m = 0; m < 5; m++)
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
            if (!GetCategorieUsed(Category.FullHouse))
            {
                int firstPairValue = 0;
                int secondPairValue = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (diceVal[i] == 2)
                    {
                        firstPairValue = i+1;
                        for (int j = 0; j < 5; j++)
                        {
                            if ((diceVal[j] == 2) && (j+1 != firstPairValue))
                            {
                                for (int k = 0; k < 5; k++)
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
            for (int i = 2; i < 6; i++)
            {
                if (diceVal[i] == 2)
                {
                    for (int j = 0; j < 5; j++)
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


/*
        public int SelectLeastValuableCat()
        {
            if (!Score.UsedCategories[0]) return 0;
            if (!Score.UsedCategories[1]) return 1;
            if (!Score.UsedCategories[2]) return 2;
            if (!Score.UsedCategories[12]) return 12;
            if (!Score.UsedCategories[11]) return 11;
            if (!Score.UsedCategories[9]) return 9;
            if (!Score.UsedCategories[6]) return 6;
            if (!Score.UsedCategories[7]) return 7;
            return 8;
        }
*/
    }
}
