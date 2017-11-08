using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Yahtzee.Model;

namespace Yahtzee.View
{
    class RoundView : Display
    {

        public void RenderRound(int roundNumber)
        {
            PrintMessage("\nRound number " + roundNumber);
        }
        public void RenderRound(string name)
        {
            PrintMessage("\n" + name + " time to play!");
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
                Console.WriteLine("Select die to roll by entering the id numbers of your choosen die separated by a space e.g.(1 2 3 5), or (0) to stand");
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

        public Category RenderCategory()
        {
            int enumLength = CategoryModel.GetSize();
            PrintMessage("\nSelect number category from this list e.g.(3): ");
            string output = "";

            foreach (Category category in CategoryModel.GetList())
            {
                output += "(" + ((int)category + 1) + ") " + category + "\n";
            }
            while (true)
            {
                int value = 0;
                Console.WriteLine(output);
                if (Int32.TryParse(Console.ReadLine(), out value) && value >= 1 && value < enumLength+1)
                {
                    return CategoryModel.GetCategory(value-1);
                }
                PrintErrorMessage("Invalid input");
            }
        }
        
        public bool ContinueGame()
        {
            do
            {
                PrintMessage("\nContinue game (y/n)");
                
                string input = Console.ReadLine().ToLower();
                if (input.CompareTo("y") == 0)
                {
                    Console.Clear();
                    return true;
                }
                else if (input.CompareTo("n") == 0)
                {
                    Console.Clear();
                    return false;
                }
                PrintErrorMessage("Invalid input, answer with (y/n).");
            } while (true);
        }

        public bool SelectActivity(string message)
        {
            do
            {
                PrintMessage(message);

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
        public void GameSaved(string fileName)
        {
            PrintMessage("Game saved: " + fileName);
        }
        public string SelectGame(FileInfo[] files)
        {
            Console.Clear();
            string selectedFile = "";
            PrintMessage("\nSelect file from list, enter number before selected file. If no files listed, press ENTER");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine("(" + i + ") " + files[i].Name);
            }
            string input = Console.ReadLine();
            int index = 0;
            if (Int32.TryParse(input, out index) && (index >= 0) && (index < files.Length))
            {
                Console.Clear();
                PrintMessage("\nGame " + files[index].Name + " selected");
                selectedFile = files[index].Name;
            }
            else
            {
                PrintErrorMessage("Not a valid input for, regarded as pressed ENTER");
            }
            return selectedFile;
        }
    }
}
