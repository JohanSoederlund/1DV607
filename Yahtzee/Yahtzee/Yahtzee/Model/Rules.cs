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
        //private int[] dieValues = { 0, 0, 0, 0, 0, 0 };
        public Rules(CollectionOfDice collectionOfDice)
        {
            this.collectionOfDice = collectionOfDice;
        }


        public int doHave(int cat)
        {
            switch (cat)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: return Number(cat);
                case 6: return ThreeOfAKind();
                case 7: return FourOfAKind();
                case 8: return FullHouse();
                case 9: return SmallStraight();
                case 10: return LargeStraight();
                case 11: return Yahtzee();
                case 12: return collectionOfDice.GetSum();

            }

            return 0;
        }

        public int ThreeOfAKind()
        {

            if (collectionOfDice.GetMaxNumberOfSameValues() >= 3)
            {
                return collectionOfDice.GetSum();
            }
            return 0;
        }
        public int FourOfAKind()
        {

            if (collectionOfDice.GetMaxNumberOfSameValues() >= 4)
            {
                return collectionOfDice.GetSum();
            }
            return 0;
        }
        public int FullHouse()
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            int retValue = 0;
            for (int i = 0; i < 5; i++)
            {
                if (diceVal[i] == 2)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (diceVal[j] == 3)
                            retValue = (i + 1) * 2 + (j + 1) * 3;
                    }
                }
            }
            return retValue;
        }

        public int SmallStraight()
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
                for (int i = 2; i < 5; i++)
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
                for (int i = 3; i < 6; i++)
                {
                    if (diceVal[i] != 1 && diceVal[i] != 2)
                    {
                        straight = false;
                    }
                }
            }

            if (straight)
            {
                retVal = collectionOfDice.GetSum();
            }
            return retVal;
        }

        public int LargeStraight()
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            if (diceVal[0] == 1)   // straight 1-5 ?
            {
                for (int i = 1; i < 5; i++)
                {
                    if (diceVal[i] != 1)
                    {
                        return 0;
                    }
                }
                return collectionOfDice.GetSum();
            }
            else if (diceVal[1] == 1)  // straight 2-6 ?
            {
                for (int i = 2; i < 6; i++)
                {
                    if (diceVal[i] != 1)
                    {
                        return 0;
                    }
                }
                return collectionOfDice.GetSum();
            }
            return 0;
        }


        public int Yahtzee()
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            int retVal = 0;
            for (int i = 0; i < 6; i++)
            {
                if (diceVal[i] == 5)
                    retVal = diceVal[i] * (i + 1);
            }
            return retVal;
        }

        public int Number(int number)
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            return diceVal[number] * (number + 1);
        }
    }

}
