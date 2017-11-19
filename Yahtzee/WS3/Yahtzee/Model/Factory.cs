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

     //   private IRules rules;
     //   private Category category;

        private GameType gameType;
   //     private CollectionOfDice collectionOfDice;
        public Factory(GameType gameType)
        {
            this.gameType = gameType;
           // this.collectionOfDice = collectionOfDice;

         //   SetRules(gameType, collectionOfDice);
        }

        public IRules GetRules(CollectionOfDice collectionOfDice)
        {
          //  return rules;

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
            //return category;
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
/*
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

    */
    }
}
