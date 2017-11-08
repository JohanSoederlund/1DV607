using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class Dice
    {
        private Random random;
        public Dice(int id)
        {
            random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            Id = id;
            Value = 0;
        }
        public int Id { get; private set; }
        public int Value { get; private set; }
        public void Roll()
        {
            Value = random.Next(1, 7);
        }
    }
}
