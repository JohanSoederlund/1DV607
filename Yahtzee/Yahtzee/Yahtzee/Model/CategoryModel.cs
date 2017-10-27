using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class CategoryModel
    {
        public static string GetName(int index) => Enum.GetName(typeof(Category), index);

        public static string GetName(Category categorie) => Enum.GetName(typeof(Category), categorie);

        public static int GetSize() => Enum.GetNames(typeof(Category)).Length;
        public static Category GetCategorie(int index) => (Category)index;

        public static Array GetList() => Enum.GetValues(typeof(Category));

      //  public static int GetIndex(Categorie categorie) => (int)categorie;
    }
}
