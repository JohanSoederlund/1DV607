using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class CategorieModel
    {
        public static string GetName(int index) => Enum.GetName(typeof(Categorie), index);

        public static string GetName(Categorie categorie) => Enum.GetName(typeof(Categorie), categorie);

        public static int GetSize() => Enum.GetNames(typeof(Categorie)).Length;
        public static Categorie GetCategorie(int index) => (Categorie)index;

        public static Array GetList() => Enum.GetValues(typeof(Categorie));

      //  public static int GetIndex(Categorie categorie) => (int)categorie;
    }
}
