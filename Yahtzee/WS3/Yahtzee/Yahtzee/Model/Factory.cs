using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Model.Rules;
using Yahtzee.Model.Categories;

namespace Yahtzee.Model
{
    class Factory
    {

        private IRules rules;
        private Category category;
        public Factory(GameType gameType, CollectionOfDice collectionOfDice)
        {


            SetRules(gameType, collectionOfDice);
        }

        public IRules GetRules()
        {
            return rules;
        }
        public Category GetCategory()
        {
            return category;
        }

        private void SetRules(GameType gameType, CollectionOfDice collectionOfDice)
        {
            if (gameType == GameType.Yahtzee)
            {
                rules = new YahtzeeRules(collectionOfDice);
                category = new CategoryYahtzee();
            } else if (gameType == GameType.Yatzy)
            {
                rules = new YatzyRules(collectionOfDice);
                category = new CategoryYatzy();
            }
        }
    }
}
