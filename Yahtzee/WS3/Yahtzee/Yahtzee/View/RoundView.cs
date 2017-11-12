using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Yahtzee.Model;
using Yahtzee.Model.Categories;

namespace Yahtzee.View
{
    enum DisplayType {ViewFullScoreBord = 0, InspectSavedGame, ResumeSavedGame, ViewAvaialbleCategories }
    class RoundView : Display
    {
        private readonly string viewFullScoreBord = "\nDo you want to view the full score board (y) or the short score board (n) of the game (y/n)";
        private readonly string inspectSavedGame = "\nDo you want to inspect a saved game (y/n)";
        private readonly string resumeSavedGame = "\nDo you want to resume a saved game (y/n)";
        private readonly string viewAvailableCategories = "\nDo you want to view available categories (y/n)";

        private Category category;
        public RoundView(Category category)
        {
            this.category = category;
        }
        public void RenderRoundNumber(int roundNumber)
        {
            PrintMessage("\nRound number " + roundNumber);
        }
        public void RenderRound(string name)
        {
            PrintMessage("\n" + name + " time to play!");
        }
        public void RenderDie(int[] die)
        {
            string idAndValueOutput = "";
            Console.WriteLine("");
            for (int i=1; i<=die.Length;i++)
            {
                idAndValueOutput += "Dice number: " + i + "     Value: " + die[i-1] + "\n";
            }
            Console.Write(idAndValueOutput);
        }
        public void RenderDieToRoll(bool[] dieToRoll, string decision="")
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
        public void RenderUnavailableCategories(List<Category.Type> unavailableCategories)
        {
            RenderCategoryList(unavailableCategories);
        }
        public Category.Type RenderCategory(List<Category.Type> unavailableCategories)
        {
            int enumLength = category.Length();
            while (true)
            {
                int value = 0;
                PrintMessage("\nSelect green marked number category from this list e.g.(3): ");
                RenderCategoryList(unavailableCategories);

                if (Int32.TryParse(Console.ReadLine(), out value) && value >= 1 && value < enumLength+1)
                {
                    bool exist = unavailableCategories.Contains(category.GetCategory(value -1));
                    if (!exist)
                        return category.GetCategory(value-1);
                }
                PrintErrorMessage("Invalid input");
            }
        }
        private void RenderCategoryList(List<Category.Type> unavailableCategories)
        {
            string output = "";
            foreach (Category.Type cat in category.GetValues())
            {
                //Category CategoryInList = availableCategories.Find(cat => cat == category);
                bool exist = unavailableCategories.Contains(cat);

                if (exist)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                output = "(" + category.GetValue(cat+ 1) + ") " + category;

                Console.WriteLine(output);
                Console.ForegroundColor = ConsoleColor.White;
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
        public bool SelectActivity(DisplayType displayType, bool ClearAtNo=true)
        {
            string message = "";
            switch (displayType)
            {
                case DisplayType.InspectSavedGame:
                    message = inspectSavedGame;
                    break;
                case DisplayType.ResumeSavedGame:
                    message = resumeSavedGame;
                    break;
                case DisplayType.ViewFullScoreBord:
                    message = viewFullScoreBord;
                    break;
                case DisplayType.ViewAvaialbleCategories:
                    message = viewAvailableCategories;
                    break;
                default:
                    break; 
            }
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
                    if (ClearAtNo)
                        Console.Clear();
                    return false;
                }
                PrintErrorMessage("Invalid input, answer with (y/n).");
            } while (true);
        }
        public void GameSaved(string fileName)
        {
            PrintMessage("Game saved: " + fileName);
        }
        public void GameFinished(string winner, int score)
        {
            PrintMessage("*************************************************");
            PrintMessage(" The winner is " + winner + " at score " + score);
            PrintMessage("*************************************************");
        }
        public string SelectGame(FileInfo[] files)
        {
            Console.Clear();
            string selectedFile = ""; 

            PrintMessage("\nSelect file from list, enter number before selected file. \nPress ANY other key to return");
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
            return selectedFile;
        }
    }
}
