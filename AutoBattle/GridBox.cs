using System;
using System.Collections.Generic;
using System.Text;
using AutoBattle.Models;

namespace AutoBattle
{
    public class GridBox
    {
        public int xIndex { get; private set; }
        public int yIndex { get; private set; }
        public Character currentCharacter { get; set; } = null;

        public GridBox(int x, int y)
        {
            xIndex = x;
            yIndex = y;
        }

        public bool IsOcupied => currentCharacter != null;
    }
}
