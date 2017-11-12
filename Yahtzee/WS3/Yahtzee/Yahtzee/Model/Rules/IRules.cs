﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Model.Categories;

namespace Yahtzee.Model.Rules
{
    public interface IRules
    {

        int GetValueForCategory(Category.Type category);

        bool HaveYahtzee();

        bool HaveLargeStraight();

        bool HaveSmallStraight();

        bool HaveFullHouse();

        bool HaveThreeOfAKind();
    }
}
