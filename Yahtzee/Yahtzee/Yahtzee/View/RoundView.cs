using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Model;

namespace Yahtzee.View
{
    class RoundView
    {

        public void RenderRound(string name, int roundNumber)
        {
            Console.WriteLine("\nRound number " + roundNumber);
            Console.WriteLine(name + " time to play!");
        }
        public void RenderDie(CollectionOfDice collectionOfDice)
        {
            string idAndValueOutput = "";
            if (collectionOfDice.Die == null)
            {
                throw new Exception("No die-list");
            }
            Console.WriteLine("");
            foreach (Dice dice in collectionOfDice.Die)
            {
                idAndValueOutput += "Dice number: " + dice.Id + "     Value: " + dice.Value + "\n";
            }
            Console.Write(idAndValueOutput);

        }

        public void RenderDieToRoll(bool[] dieToRoll, string decision)
        {
            bool stand = true;
            for (int i = 0; i < dieToRoll.Length; i++)
            {
                if (dieToRoll[i])
                    stand = false;
            }
            if (stand)
                Console.Write("\n" + decision );
            else
            {
                Console.Write("\n" + decision + " - Decided to reroll: ");
                for (int i = 0; i < dieToRoll.Length; i++)
                {
                    if (dieToRoll[i])
                    {
                        Console.Write(i + 1 + " ");
                    }
                }
                Console.WriteLine("");
            }
        }

        public bool[] GetDieToRoll()
        {
            bool[] dieToRoll = { };
            bool getInput = true;
            int val;
            int index;
            while (getInput)
            {
                dieToRoll = new bool[] { false, false, false, false, false };
                Console.WriteLine("Select die to roll på entering the id numbers of your choosen die e.g.(1 2 3 5), or (0) to stand");
                string input = Console.ReadLine();
                string[] dieNumbers = input.Split(' ');
                getInput = false;
                //Check if player stand
                if (Int32.TryParse(dieNumbers[0], out val) && val == 0)
                {
                    return dieToRoll;
                }
                for (int i = 0; i < dieNumbers.Length; i++)
                {
                    Console.WriteLine(dieNumbers[i]);
                    if (Int32.TryParse(dieNumbers[i], out index) && index >= 1 && index <= 5)
                    {
                        dieToRoll[index - 1] = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                        getInput = true;
                        break;
                    }
                }
            }
            return dieToRoll;
        }

        public Categorie RenderCategorie(bool[] usedCategories)
        {
            int value;
            int enumLength = Enum.GetNames(typeof(Categorie)).Length;
            string output = "Select number categorie from this list e.g.(3): \n";
            for (int i = 0; i < enumLength; i++)
            {
                output += "(" + i + ") " + Enum.GetName(typeof(Categorie), i) + "\n";
            }
            while (true)
            {
                Console.WriteLine(output);
                if (Int32.TryParse(Console.ReadLine(), out value) && value >= 0 && value < enumLength)
                {
                    return (Categorie)value;
                }
                Console.WriteLine("Invalid input");
            }
        }

        public void RenderRoundScore(int roundScore, int usedCategorie)
        {
            Console.WriteLine("Recieved " + roundScore + " points for categorie " + CategorieModel.GetName(usedCategorie));
        }
        public bool ContinueGame()
        {
            
            do
            {
                Console.WriteLine("Continue game (y/n)");
                string input = Console.ReadLine().ToLower();
                if (input.CompareTo("y") == 0)
                {
                    return true;
                }
                else if (input.CompareTo("n") == 0)
                {
                    Console.WriteLine("Game will be saved");
                    return false;
                }
                Console.WriteLine("Invalid input, answer with (y/n).");
            } while (true);
        }

        public bool ResumeGame()
        {

            do
            {
                Console.WriteLine("Resume saves game (y/n)");
                string input = Console.ReadLine().ToLower();
                if (input.CompareTo("y") == 0)
                {
                    return true;
                }
                else if (input.CompareTo("n") == 0)
                {
                    return false;
                }
                Console.WriteLine("Invalid input, answer with (y/n).");
            } while (true);
        }

    }
}
