
namespace Yahtzee.Model.Rules
{
    class BaseRules
    {
        private CollectionOfDice collectionOfDice;

        public BaseRules(CollectionOfDice collectionOfDice)
        {
            this.collectionOfDice = collectionOfDice;
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

        public bool HaveTwoPair()
        {
            int[] diceVal = collectionOfDice.GetNumberOfDiceFaceValue();
            bool retValue = false;
            for (int i = 0; i < diceVal.Length; i++)
            {
                if (diceVal[i] >= 2)
                {
                    for (int j = i + 1; j < diceVal.Length; j++)
                    {
                        if (diceVal[j] >= 2)
                            retValue = true;
                    }
                }
            }
            return retValue;
        }

        public bool HavePair()
        {
            if (collectionOfDice.GetMaxNumberOfSameValues() >= 2)
            {
                return true;
            }
            return false;
        }
    }
}