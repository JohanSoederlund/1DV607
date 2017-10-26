using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class Score
    {

        public Score()
        {
            UsedCategories = new bool[CategorieModel.GetSize()];
            ScoreCard = new int[CategorieModel.GetSize()];
            foreach (Categorie categorie in CategorieModel.GetList())
            {
                SetUsedCategorie(categorie, false);
                SetScoreInScoreCard(categorie, 0);
            }
        }

        public Score(int[] scores, bool[] usedCategories)
        {
            UsedCategories = usedCategories;
            ScoreCard = scores;
        }

        public int GetTotalScore()
        {
            int totalScore = 0;
            for (int i = 0; i < ScoreCard.Length; i++)
            {
                totalScore += ScoreCard[i];
            }
            return totalScore;
        }
        private int[] ScoreCard { get; set;}

        public int[] GetScoreCard()
        {
            int[] ScoreCardCopy = new int[CategorieModel.GetSize()];
            ScoreCard.CopyTo(ScoreCardCopy, 0);
            return ScoreCardCopy;
        }

        public int GetScoreInScoreCard(Categorie cat)
        {
            return ScoreCard[(int)cat];
        }
        public void SetScoreInScoreCard(Categorie categorieToUse, int value)
        {
            ScoreCard[(int)categorieToUse] = value; 
        }

        public bool[] GetUsedCategories()
        {
            bool[] UsedCategoriesCopy = new bool[CategorieModel.GetSize()];
            UsedCategories.CopyTo(UsedCategoriesCopy, 0);
            return UsedCategoriesCopy;
        }
  
         


        private bool[] UsedCategories { get; set; }
        public bool GetUsedCategorie(Categorie categorieToUse)
        {
            return UsedCategories[(int)categorieToUse];
        }

        public void SetUsedCategorie(Categorie categorieToUse, bool value)
        {
            UsedCategories[(int)categorieToUse] = value;
        }
        public int GetNumberOfCategories()
        {
            return UsedCategories.Length;
        }
        public int GetNumberOfUsedCategories()
        {
            int rounds = 0;
            foreach (Categorie cat in Enum.GetValues(typeof(Categorie)))
            {
                if (GetUsedCategorie(cat))
                {
                    rounds++;
                }
            }
            return rounds;
        }
    }
}


/*
 * 
 * public int Twos { get; private set; }

        public int Threes { get; private set; }

        public int Fours { get; private set; }

        public int Fives{ get; private set; }

        public int Sixes{ get; private set; }

        public int ThreeOfAKind { get; private set; }

        public int ThreOfAKind { get; private set; }

        public int ThreOfAKind { get; private set; }

        public int ThreOfAKind { get; private set; }

        public int ThreOfAKind { get; private set; }

        public int ThreOfAKind { get; private set; }

        public int ThreOfAKind { get; private set; }
*/