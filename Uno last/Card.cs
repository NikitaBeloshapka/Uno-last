﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сards
{
    public class Card
    {
        public Card(CardColour colour,KindsOfCards kinds)
        {
            Colour = colour;
            Kinds = kinds;
        }
        public CardColour Colour { get; set; }
        public KindsOfCards Kinds { get; set; }
        public virtual void Show()
        {
            Console.WriteLine(this);
        }
        public override string ToString()
        {
            return String.Format("{0},{1}", Colour,Kinds);
        }
    }
}

public enum CardColour
{
    red,
    blue,
    green,
    yellow,
    black
}

public enum KindsOfCards
{
    zero,
    one,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine,
    skip,
    reverse,
    add2
}