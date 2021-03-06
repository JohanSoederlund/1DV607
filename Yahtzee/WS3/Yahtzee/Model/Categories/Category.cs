﻿using System;

namespace Yahtzee.Model.Categories
{
    public abstract class Category
    {
        public enum Type { }

        abstract public string GetName(Type type);

        abstract public string GetName(object type);

        abstract public string GetName(int index);

        abstract public string[] GetNames();

        abstract public Array GetValues();

        abstract public int Length();

        abstract public Category.Type[] Categories();

        abstract public int GetValue(Type type);

        abstract public int GetValue(object type);

        public abstract Type GetCategory(int i);

        abstract public Type SmallStraight();

        abstract public Type LargeStraight();

        abstract public Type FullHouse();

        abstract public Type Yahtzee();

        abstract public Type Threes();

        abstract public Type Sixes();

        abstract public Type Chance();
    }

}
