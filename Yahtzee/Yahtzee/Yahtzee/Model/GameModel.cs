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
            Date = DateTime.Now;
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
            protected set
            {
                _date = value;
            }
        }
    }
}
