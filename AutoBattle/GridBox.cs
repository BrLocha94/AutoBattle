using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class GridBox
    {
        public int xIndex { get; private set; }
        public int yIndex { get; private set; }
        public bool ocupied { get; set; }

        public GridBox(int x, int y, bool ocupied)
        {
            xIndex = x;
            yIndex = y;
            this.ocupied = ocupied;
        }
    }
}
