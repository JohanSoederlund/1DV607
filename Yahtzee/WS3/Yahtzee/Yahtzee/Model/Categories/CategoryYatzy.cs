using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model.Categories
{
    class CategoryYatzy : Category
    {

        new public enum Type
        {
            Aces =0, Twos, Threes, Fours, Fives, Sixes, Pair, TwoPair, ThreeOfAKind, FourOfAKind, FullHouse, SmallStraight, LargeStraight, Yahtzee, Chance
        }

        public override int Length() { return Enum.GetNames(typeof(Type)).Length; }
        public override Category.Type[] Categories() { return (Category.Type[])Enum.GetValues(typeof(Type)); }
        public override string[] GetNames() { return Enum.GetNames(typeof(Type)); }

        public override string GetName(Category.Type i) { return Enum.GetName(typeof(Type), i); }
        public override string GetName(int i) { return Enum.GetName(typeof(Type), i); }
        public override string GetName(object i) { return Enum.GetName(typeof(Type), i); }

        public override Array GetValues() { return Enum.GetValues(typeof(Type)); }


        //public override Type GetCategory(int i) { return System.Enum.GetName(typeof(Type), i); }

        public override int GetValue(Category.Type type) { return (int)type; }

        public override int GetValue(object type) { return (int)type; }

        public override Category.Type GetCategory(int i) { return (Category.Type)i; }
    }
}



