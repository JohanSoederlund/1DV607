using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Model;

namespace Yahtzee.View
{
    class RoundView : Display
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
                Console.Write("\n" + decision + "\n");
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
                Console.WriteLine("Select die to roll by entering the id numbers of your choosen die e.g.(1 2 3 5), or (0) to stand");
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
                    if (Int32.TryParse(dieNumbers[i], out index) && index >= 1 && index <= 5)
                    {
                        dieToRoll[index - 1] = true;
                    }
                    else
                    {
                        PrintErrorMessage("Invalid input");
                        getInput = true;
                        break;
                    }
                }
            }
            return dieToRoll;
        }

        public Categorie RenderCategorie()
        {
            int enumLength = CategorieModel.GetSize();
            string output = "Select number categorie from this list e.g.(3): \n";
            for (int i = 0; i < enumLength; i++)
            {
                
            }

            foreach (Categorie categorie in CategorieModel.GetList())
            {
                output += "(" + (int)categorie + ") " + categorie + "\n";
            }
            while (true)
            {
                Console.WriteLine(output);
                if (Int32.TryParse(Console.ReadLine(), out int value) && value >= 0 && value < enumLength)
                {
                    return CategorieModel.GetCategorie(value);
                }
                PrintErrorMessage("Invalid input");
            }
        }
        
        public bool ContinueGame()
        {
            do
            {
                PrintMessage("Continue game (y/n)");
                
                string input = Console.ReadLine().ToLower();
                if (input.CompareTo("y") == 0)
                {
                    Console.Clear();
                    return true;
                }
                else if (input.CompareTo("n") == 0)
                {
                    Console.Clear();
                    PrintMessage("Game will be saved");
                    return false;
                }
                PrintErrorMessage("Invalid input, answer with (y/n).");
            } while (true);
        }

        public bool ResumeGame()
        {
            do
            {
                PrintMessage("Do you want to resume last instance of saved game (y/n)");

                string input = Console.ReadLine().ToLower();
                if (input.CompareTo("y") == 0)
                {
                    return true;
                }
                else if (input.CompareTo("n") == 0)
                {
                    return false;
                }
                PrintErrorMessage("Invalid input, answer with (y/n).");
            } while (true);
        }

    }
}
