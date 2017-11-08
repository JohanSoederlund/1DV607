using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class Rules
    {
        private CollectionOfDice collectionOfDice;
        private const int yahtzeeValue = 50;
        private const int largeStraightValue = 40;
        private const int smallStraightValue = 30;
        private const int fullHouseValue = 25;

        //private int[] dieValues = { 0, 0, 0, 0, 0, 0 };
        public Rules(CollectionOfDice collectionOfDice)
        {
            this.collectionOfDice = collectionOfDice;
        }

        public int GetValueForCategory(Category category)
        {
            switch (category)
            {
                case Category.Aces:
                case Category.Twos:
                case Category.Threes:
                case Category.Fours:
                case Category.Fives:
                case Category.Sixes: return SumOfSameCategory(category);
                case Category.ThreeOfAKind: return ThreeOfAKind();
                case Category.FourOfAKind: return FourOfAKind();
                case Category.FullHouse: return FullHouse();
                case Category.SmallStraight: return SmallStraight();
                case Category.LargeStraight: return LargeStraight();
                case Category.Yahtzee: return Yahtzee();
                case Category.Chance: return collectionOfDice.GetSum();
            }
            return 0;
        }

        private int ThreeOfAKind()
        {

            if (collectionOfDice.GetMaxNumberOfSameValues() >= 3)
            {
                return collectionOfDice.GetSum();
            }
            return 0;
        }
        private int FourOfAKind()
        {

            if (collectionOfDice.GetMaxNumberOfSameValues() >= 4)
            {
                return collectionOfDice.GetSum();
            }
            return 0;
        }
        private int FullHouse()
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            int retValue = 0;
            for (int i = 0; i < diceVal.Length; i++)
            {
                if (diceVal[i] == 2)
                {
                    for (int j = 0; j < diceVal.Length; j++)
                    {
                        if (diceVal[j] == 3)
                            retValue = fullHouseValue;
                    }
                }
            }
            return retValue;
        }

        private int SmallStraight()
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            int retVal = 0;
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

            if (straight)
            {
                retVal = smallStraightValue;
            }
            return retVal;
        }

        private int LargeStraight()
        {
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            if (diceValue[0] == 1)   // straight 1-5 ?
            {
                for (int i = 0; i < diceValue.Length-1; i++)
                {
                    if (diceValue[i] != 1)
                    {
                        return 0;
                    }
                }
                return largeStraightValue;
            }
            else if (diceValue[1] == 1)  // straight 2-6 ?
            {
                for (int i = 1; i < diceValue.Length; i++)
                {
                    if (diceValue[i] != 1)
                    {
                        return 0;
                    }
                }
                return largeStraightValue;
            }
            return 0;
        }


        private int Yahtzee()
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            int retVal = 0;
            for (int i = 0; i < diceVal.Length; i++)
            {
                if (diceVal[i] == 5)
                    retVal = yahtzeeValue;
            }
            return retVal;
        }

        private int SumOfSameCategory(Category category)
        {
            int faceValue = (int)category + 1;
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            return diceValue[faceValue - 1] * (faceValue);
        }
    }

}
