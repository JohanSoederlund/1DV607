﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Model.Categories
{
    abstract class Category
    {

        public enum Type { }

        abstract public string GetName(Type type);

        abstract public string GetName(object type);

        abstract public string GetName(int index);

        abstract public string[] GetNames();

        abstract public Array GetValues();

        abstract public int Length();

        //abstract public Type GetCategory(int index);


        abstract public Category.Type[] Categories();

        abstract public int GetValue(Type type);

        abstract public int GetValue(object type);

        //abstract public int GetValue(int index);

        public abstract Type GetCategory(int i);

    }

}
