using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model
{
    class CategoryModel
    {
        public static string GetName(Category category) => Enum.GetName(typeof(Category), category);

        public static int GetSize() => Enum.GetNames(typeof(Category)).Length;
        public static Category GetCategory(int index) => (Category)index;

        public static Array GetList() => Enum.GetValues(typeof(Category));
    }
}
