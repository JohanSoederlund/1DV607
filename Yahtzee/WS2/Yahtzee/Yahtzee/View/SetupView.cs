﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Yahtzee.View
{
    class SetupView : Display
    {
        private readonly string mainMenu = "Welcome to Yahtzee! \n\n";
        private readonly string rules = "Rules can be found at https://en.wikipedia.org/wiki/Yahtzee \n\n";
        public SetupView()
        {
            Console.WriteLine(mainMenu);
            Console.WriteLine(rules);
        }
        public int NumberOfPlayers()
        {
            int value = 0;
            Console.WriteLine("How many players (1-5): ");
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out value) && value <= 5 && value >= 1)
                {
                    return value;
                }
                Console.WriteLine("Invalid input value, you need to give a value between 1 and 5.");
            }
        }
        public string PlayerName(int number)
        {
            do{
                Console.WriteLine("Player number " + number + ": Player what is your name (3-8 characters): ");
                string input = Console.ReadLine().ToLower();
                if (input.Length <= 8 && input.Length >= 3)
                {
                    return input;
                }
                Console.WriteLine("Invalid input.");
            } while (true);
        }
        public bool IsRobot(int number)
        {
            do
            {
                Console.WriteLine("Player number " + number + ": Is this player a robot (y/n)");
                string input = Console.ReadLine().ToLower();
                if (input.CompareTo("y") == 0)
                {
                    Console.WriteLine("A robot player created successfully");
                    Thread.Sleep(1000);
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
