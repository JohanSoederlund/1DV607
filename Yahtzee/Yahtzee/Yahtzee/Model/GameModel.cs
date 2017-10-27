using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class GameModel
    {

        private DateTime _date;

        public GameModel()
        {
            RoundNumber = 0;
            Date = new DateTime();
        }

        public int RoundNumber{ get; protected set; }

        protected void IncrementRoundNumber()
        {
            RoundNumber++;
        }

        public DateTime Date{
            get
            {
                return _date;
            }
            private set
            {
                _date = value;
            }
        }
    }
}
