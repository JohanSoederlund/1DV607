using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model.Rules
{
    public class Rules
    {
        private CollectionOfDice collectionOfDice;
        private const int yahtzeeValue = 50;
        private const int largeStraightValue = 40;
        private const int smallStraightValue = 30;
        private const int fullHouseValue = 25;

        public Rules(CollectionOfDice collectionOfDice)
        {
            this.collectionOfDice = collectionOfDice;
        }

        public int GetValueForCategory(Category category)
        {
            int retValueForCategory = 0;  // Default value if condition for category not met
            switch (category)
            {
                case Category.Aces:
                case Category.Twos:
                case Category.Threes:
                case Category.Fours:
                case Category.Fives:
                case Category.Sixes: 
                            retValueForCategory = SumOfSameCategory(category);
                            break;
                case Category.ThreeOfAKind:
                            if (HaveThreeOfAKind())
                                retValueForCategory = ThreeOfAKind();
                            break;
                case Category.FourOfAKind:
                            if (HaveFourOfAKind())
                                retValueForCategory = FourOfAKind();
                            break;
                case Category.FullHouse: 
                            if (HaveFullHouse())
                                retValueForCategory = FullHouse();
                            break;
                case Category.SmallStraight:
                            if (HaveSmallStraight())
                                retValueForCategory = SmallStraight();
                            break;
                case Category.LargeStraight:
                            if (HaveLargeStraight())
                                retValueForCategory = LargeStraight();
                            break;
                case Category.Yahtzee:
                            if (HaveYahtzee())
                                retValueForCategory = Yahtzee();
                            break;
                case Category.Chance:
                            retValueForCategory = collectionOfDice.GetSum();
                            break;
            }
            return retValueForCategory;
        }
        public bool HaveYahtzee()
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            bool retVal = false;
            for (int i = 0; i < diceVal.Length; i++)
            {
                if (diceVal[i] == 5)
                    retVal = true;
            }
            return retVal;
        }
        public bool HaveLargeStraight()
        {
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            if (diceValue[0] == 1)   // straight 1-5 ?
            {
                for (int i = 0; i < diceValue.Length - 1; i++)
                {
                    if (diceValue[i] != 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (diceValue[1] == 1)  // straight 2-6 ?
            {
                for (int i = 1; i < diceValue.Length; i++)
                {
                    if (diceValue[i] != 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public bool HaveSmallStraight()
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            bool straight = false;
            if (diceVal[0] == 1 || diceVal[0] == 2)   // straight 1-4 ?
            {
                straight = true;
                for (int i = 1; i < 4; i++)
                {
                    if (diceVal[i] != 1 && diceVal[i] != 2)
                    {
                        straight = false;
                    }
                }

            }
            if (!straight && (diceVal[1] == 1 || diceVal[1] == 2))   // straight 2-5 ?
            {
                straight = true;
                for (int i = 2; i < diceVal.Length; i++)
                {
                    if (diceVal[i] != 1 && diceVal[i] != 2)
                    {
                        straight = false;
                    }
                }
            }
            if (!straight && (diceVal[2] == 1 || diceVal[2] == 2))   // straight 3-6 ?
            {
                straight = true;
                for (int i = 3; i < diceVal.Length; i++)
                {
                    if (diceVal[i] != 1 && diceVal[i] != 2)
                    {
                        straight = false;
                    }
                }
            }
            return straight;
        }
        public bool HaveFullHouse()
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            bool retValue = false;
            for (int i = 0; i < diceVal.Length; i++)
            {
                if (diceVal[i] == 2)
                {
                    for (int j = 0; j < diceVal.Length; j++)
                    {
                        if (diceVal[j] == 3)
                            retValue = true;
                    }
                }
            }
            return retValue;
        }
        public bool HaveThreeOfAKind()
        {

            if (collectionOfDice.GetMaxNumberOfSameValues() >= 3)
            {
                return true;
            }
            return false;
        }
        public bool HaveFourOfAKind()
        {

            if (collectionOfDice.GetMaxNumberOfSameValues() >= 4)
            {
                return true;
            }
            return false;
        }

        private int ThreeOfAKind()
        {
            return collectionOfDice.GetSum();
        }
        private int FourOfAKind()
        {
            return collectionOfDice.GetSum();
        }
        private int FullHouse()
        {
            return fullHouseValue;
        }
        private int SmallStraight()
        {
            return smallStraightValue;
        }
        private int LargeStraight()
        {
            return largeStraightValue;
        }
        private int Yahtzee()
        {
            return yahtzeeValue;
        }
        private int SumOfSameCategory(Category category)
        {
            int faceValue = (int)category + 1;
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            return diceValue[faceValue - 1] * (faceValue);
        }


    }

}
