using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model.Categories
{
    class CategoryYahtzee : Category
    {

        new public enum Type
        {
            Aces = 0, Twos, Threes, Fours, Fives, Sixes, ThreeOfAKind, FourOfAKind, FullHouse, SmallStraight, LargeStraight, Yahtzee, Chance,
        }

        public override int Length() { return Enum.GetNames(typeof(Type)).Length; }
        public override Category.Type[] Categories() { return (Category.Type[])Enum.GetValues(typeof(Type)); }
        public override string[] GetNames() { return Enum.GetNames(typeof(Type)); }

        public override string GetName(Category.Type i) { return Enum.GetName(typeof(Type), i); }
        public override string GetName(int i) { return Enum.GetName(typeof(Type), i); }
        public override string GetName(object i) { return Enum.GetName(typeof(Type), i); }

        public override Array GetValues() { return Enum.GetValues(typeof(Type)); }


        public override Category.Type GetCategory(int i) { return (Category.Type)i; }


        public override int GetValue(Category.Type type) { return (int)type; }

        public override int GetValue(object type) { return (int)type; }


        public override Category.Type Yahtzee()
        {
            return (Category.Type)Type.Yahtzee;
        }
        public override Category.Type SmallStraight()
        {
            return (Category.Type)Type.SmallStraight;
        }
        public override Category.Type LargeStraight()
        {
            return (Category.Type)Type.LargeStraight;
        }
        public override Category.Type FullHouse()
        {
            return (Category.Type)Type.FullHouse;
        }
        public override Category.Type Chance()
        {
            return (Category.Type)Type.Chance;
        }
        public override Category.Type Threes()
        {
            return (Category.Type)Type.Threes;
        }
        public override Category.Type Sixes()
        {
            return (Category.Type)Type.Sixes;
        }
    }
}
