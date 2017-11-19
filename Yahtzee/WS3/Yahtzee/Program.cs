using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Controller;

using System.Web.Script.Serialization;

namespace Yahtzee
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game game = new Game();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
