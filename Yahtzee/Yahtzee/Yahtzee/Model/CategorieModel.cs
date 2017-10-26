using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class CategorieModel
    {


        public static string GetName(int index)
        {
            return Enum.GetName(typeof(Categorie), index);
        }

        public static string GetName(Categorie categorie)
        {
            return Enum.GetName(typeof(Categorie), categorie);
        }

        public static int GetSize()
        {
            return Enum.GetNames(typeof(Categorie)).Length;
        }
    }
}
