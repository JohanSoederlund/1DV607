using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class CollectionOfDice
    {
        public const int NoOfDie = 5;
        private List<IDieObserver> observers = new List<IDieObserver>();
        public CollectionOfDice()
        {
            Die = new List<Dice>();
            for (int i = 1; i <= NoOfDie; i++)
            {
                Thread.Sleep(20);
                Die.Add(new Dice(i));
            }
        }
        public void Subscribe(IDieObserver observer)
        {
            observers.Add(observer);
        }
        private void NotifyAll()
        {
            foreach (var observer in observers)
            {
                if (observer != null) observer.DieRolled(GetNumberOfDiceFaceValue(), GetDie());
            }
        }

        public List<Dice> Die { get; private set; }

        public void Roll(bool[] dieToRoll)
        {
            foreach (Dice dice in Die)
            {
                if (dieToRoll[dice.Id-1])
                {
                    dice.Roll();
                }
            }
            NotifyAll();
        }

        public int[] GetDie()
        {
            int[] dieValues = { 0, 0, 0, 0, 0 };
            int index = 0;
            foreach (Dice dice in Die)
            {
                dieValues[index++] = dice.Value;
            }
            return dieValues;
        }
        public int[] GetNumberOfDiceFaceValue()
        {
            int[] dieValues = { 0, 0, 0, 0, 0, 0 };
            foreach (Dice dice in Die)
            {
                dieValues[dice.Value - 1]++;
            }
            return dieValues;
        }
        public int GetMaxNumberOfSameValues()
        {
            // Return total value if at least three dice of same, else 0
            int[] dieValues = GetNumberOfDiceFaceValue();
            int highestNumberOfSame = 0;
            for (int i = 0; i < 6; i++)
            {
                if (dieValues[i] > highestNumberOfSame)
                    highestNumberOfSame = dieValues[i];
            }
            return highestNumberOfSame;
        }
        public int GetSum()
        {
            int sum = 0;
            foreach (Dice dice in Die)
            {
                sum += dice.Value;
            }
            return sum;
        }
    }
}
