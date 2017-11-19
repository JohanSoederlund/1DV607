using Yahtzee.Model.Categories;

namespace Yahtzee.Model.Rules
{
    class YatzyRules : IRules
    {
        private CollectionOfDice collectionOfDice;
        private const int yahtzeeValue = 50;
        private const int largeStraightValue = 20;
        private const int smallStraightValue = 15;

        public YatzyRules(CollectionOfDice collectionOfDice)
        {
            this.collectionOfDice = collectionOfDice;
            BaseRules = new BaseRules(collectionOfDice);
        }

        public BaseRules BaseRules { get; set; }

        public int GetValueForCategory(Category.Type category)
        {
            CategoryYatzy.Type categoryYatzy = (CategoryYatzy.Type)category;
            int retValueForCategory = 0;  // Default value if condition for category not met

            switch (categoryYatzy)
            {
                case CategoryYatzy.Type.Aces:
                case CategoryYatzy.Type.Twos:
                case CategoryYatzy.Type.Threes:
                case CategoryYatzy.Type.Fours:
                case CategoryYatzy.Type.Fives:
                case CategoryYatzy.Type.Sixes:
                    retValueForCategory = SumOfSameCategory(categoryYatzy);
                    break;
                case CategoryYatzy.Type.Pair:
                    if (BaseRules.HavePair())
                        retValueForCategory = Pair();
                    break;
                case CategoryYatzy.Type.TwoPair:
                    if (BaseRules.HaveTwoPair())
                        retValueForCategory = TwoPair();
                    break;
                case CategoryYatzy.Type.ThreeOfAKind:
                    if (BaseRules.HaveThreeOfAKind())
                        retValueForCategory = ThreeOfAKind();
                    break;
                case CategoryYatzy.Type.FourOfAKind:
                    if (BaseRules.HaveFourOfAKind())
                        retValueForCategory = FourOfAKind();
                    break;
                case CategoryYatzy.Type.FullHouse:
                    if (BaseRules.HaveFullHouse())
                        retValueForCategory = FullHouse();
                    break;
                case CategoryYatzy.Type.SmallStraight:
                    if (HaveSmallStraight())
                        retValueForCategory = SmallStraight();
                    break;
                case CategoryYatzy.Type.LargeStraight:
                    if (HaveLargeStraight())
                        retValueForCategory = LargeStraight();
                    break;
                case CategoryYatzy.Type.Yahtzee:
                    if (BaseRules.HaveYahtzee())
                        retValueForCategory = Yahtzee();
                    break;
                case CategoryYatzy.Type.Chance:
                    retValueForCategory = collectionOfDice.GetSum();
                    break;
            }
            return retValueForCategory;
        }

        private int Pair()
        {
            // Select highest pair
            int totalValue = 0;
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            int die = diceValue.Length;
            //            return diceValue[faceValue - 1] * (faceValue);

            for (int i = die; i > 0; i--)
            {
                if (diceValue[i - 1] > 1)
                    totalValue = i * 2;
            }
            return totalValue;
        }

        private int TwoPair()
        {
            int totalValue = 0;
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            int die = diceValue.Length;
            //            return diceValue[faceValue - 1] * (faceValue);

            for (int i = die; i > 0; i--)
            {
                if (diceValue[i - 1] > 1)
                    totalValue += i * 2;
            }
            return totalValue;
        }

        private int ThreeOfAKind()
        {
            // Select highest pair
            int totalValue = 0;
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            int die = diceValue.Length;
            //            return diceValue[faceValue - 1] * (faceValue);

            for (int i = die; i > 0; i--)
            {
                if (diceValue[i - 1] > 2)
                    totalValue = i * 3;
            }
            return totalValue;
        }

        private int FourOfAKind()
        {
            // Select highest pair
            int totalValue = 0;
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            int die = diceValue.Length;
            //            return diceValue[faceValue - 1] * (faceValue);

            for (int i = die; i > 0; i--)
            {
                if (diceValue[i - 1] > 3)
                    totalValue = i * 4;
            }
            return totalValue;
        }

        private int FullHouse()
        {
            return collectionOfDice.GetSum();
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

        private int SumOfSameCategory(CategoryYatzy.Type category)
        {
            int faceValue = (int)category + 1;
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            return diceValue[faceValue - 1] * (faceValue);
        }

        public bool HaveLargeStraight()
        {
            // straight 2-6 
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            if (diceValue[1] == 1)
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
            int[] diceValue = collectionOfDice.GetNumberOfDiceFaceValue();
            if (diceValue[0] == 1)   // straight 1-5
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
            return false;
        }
    }
}
