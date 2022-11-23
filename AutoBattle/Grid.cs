using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AutoBattle
{
    public class Grid
    {
        private GridBox[,] grids;
        public int xLength { get; private set; }
        public int yLength { get; private set; }
        public int totalSize => xLength + yLength;

        public Grid(int Lines, int Columns)
        {
            xLength = Lines;
            yLength = Columns;

            grids = new GridBox[xLength, yLength];

            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false);
                    grids[i, j] = newBox;
                    Console.Write($"{newBox.xIndex}, {newBox.yIndex}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield()
        {
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    GridBox currentgrid = grids[i, j];

                    if (currentgrid.ocupied)
                        Console.Write("[X]\t");
                    else
                        Console.Write($"[ ]\t");
                }

                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            Console.Write(Environment.NewLine + Environment.NewLine);
        }

        public GridBox GetRandomFreeLocation()
        {
            Random rand = new Random();

            int randomLocationX = 0;
            int randomLocationY = 0;

            bool isFree = false;

            while (!isFree)
            {
                randomLocationX = rand.Next(0, xLength);
                randomLocationY = rand.Next(0, yLength);

                if(!grids[randomLocationX, randomLocationY].ocupied)
                    isFree = true;
            }

            return grids[randomLocationX, randomLocationY];
        }

        public GridBox GetLocation(int row, int column)
        {
            if (!CheckBounds(row, column)) return null;

            return grids[row, column];
        }

        public bool CheckBounds(int row, int column)
        {
            if (row < 0 || row >= xLength) return false;

            if (column < 0 || column >= yLength) return false;

            return true;
        }
    }
}
