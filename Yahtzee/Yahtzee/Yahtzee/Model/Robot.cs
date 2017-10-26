using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class Robot : Player
    {
        Rules rules;
        
        public Robot(int id, Rules rules)
            : base("Robot" + id, true)
        {
            this.rules = rules;
        }
        public Robot(string name, Rules rules, int[] scores, bool[] usedCategories)
            : base(name, true, scores, usedCategories)
        {
            this.rules = rules;
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



        public Categorie CalcBestValue()
        {
            //intentially fall through all options to find best value
            int high = 0;
            Categorie highCat = 0;
            int[] possible = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


            foreach (Categorie cat in CategorieModel.GetList())
            {
                int i = (int)cat;
                possible[i] = rules.doHave(cat);
                if (cat != Categorie.Chance && !Score.GetUsedCategorie(cat) && possible[i] >= high)  // always chose the highest score and if many on same highest cat
                {
                    high = possible[i];
                    highCat = cat;
                    //Console.WriteLine("New HIGH: " + high + "   CAT: " + highCat);
                }
            }

            possible[12] = rules.doHave(Categorie.Chance);
            if (!Score.GetUsedCategorie(Categorie.Chance) && possible[12] > high && high < 10 && highCat > Categorie.Threes && highCat < Categorie.Yahtzee)  // Only chance if nothing better or equal
            {
                high = possible[12];
                highCat = Categorie.Chance;
            }

            Score.SetUsedCategorie(highCat, true);
            Score.SetScoreInScoreCard(highCat, high);
            return highCat;
        }

        private bool Stand()
        {
            if ((rules.Yahtzee() > 0) && !Score.GetUsedCategorie(Categorie.Yahtzee) ||
                (rules.FullHouse() > 0) && !Score.GetUsedCategorie(Categorie.FullHouse) ||
                (rules.LargeStraight() > 0) && !Score.GetUsedCategorie(Categorie.LargeStraight) ||
                (rules.SmallStraight() > 0) && !Score.GetUsedCategorie(Categorie.SmallStraight))
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
            if (!(Score.GetUsedCategorie(Categorie.SmallStraight) && Score.GetUsedCategorie(Categorie.LargeStraight)))
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
            if (!Score.GetUsedCategorie(Categorie.FullHouse))
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
                            if ((diceVal[j] == 2) && (j != firstPairValue))
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
