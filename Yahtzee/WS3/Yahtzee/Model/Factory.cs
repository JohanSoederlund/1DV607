using Yahtzee.Model.Rules;
using Yahtzee.Model.Categories;

namespace Yahtzee.Model
{
    class Factory
    {
        private GameType gameType;
        public Factory(GameType gameType)
        {
            this.gameType = gameType;
        }

        public IRules GetRules(CollectionOfDice collectionOfDice)
        {
            if (gameType == GameType.Yahtzee)
            {
               return new YahtzeeRules(collectionOfDice);
            }
            else if (gameType == GameType.Yatzy)
            {
                return new YatzyRules(collectionOfDice);
            }
            return null;
        }

        public Category GetCategory()
        {
            if (gameType == GameType.Yahtzee)
            {
                return new CategoryYahtzee();
            }
            else if (gameType == GameType.Yatzy)
            {
                return new CategoryYatzy();
            }
            return null;
        }
    }
}
