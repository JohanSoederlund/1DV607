using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    interface IDieObserver
    {
        void DieRolled(int[] dieValues, int[] die);
    }
}
