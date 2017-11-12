using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Model.Rules;

namespace Yahtzee.Model
{
    public class Factory
    {

        private IRules rules;
        public Factory(GameType gameType, CollectionOfDice collectionOfDice)
        {
            SetRules(gameType, collectionOfDice);
        }

        public IRules GetRules()
        {
            return rules;
        }

        private void SetRules(GameType gameType, CollectionOfDice collectionOfDice)
        {
            if (gameType == GameType.Yahtzee)
            {
                rules = new YahtzeeRules(collectionOfDice);
            } else if (gameType == GameType.Yatzy)
            {
                rules = new YatzyRules();
            }
        }
    }
}
