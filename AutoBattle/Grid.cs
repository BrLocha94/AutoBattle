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
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    GridBox newBox = new GridBox(i, j);
                    grids[i, j] = newBox;
                    Console.Write($"{newBox.xIndex}, {newBox.yIndex}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield()
        {
            for (int j = 0; j < yLength; j++)
            {
                for (int i = 0; i < xLength; i++)
                {
                    GridBox currentgrid = grids[i, j];

                    if (currentgrid.IsOcupied)
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

                if(!grids[randomLocationX, randomLocationY].IsOcupied)
                    isFree = true;
            }

            return grids[randomLocationX, randomLocationY];
        }

        public GridBox GetLocation(int xIndex, int yIndex)
        {
            if (!CheckBounds(xIndex, yIndex)) return null;

            return grids[xIndex, yIndex];
        }

        public bool CheckBounds(int xIndex, int yIndex)
        {
            if (xIndex < 0 || xIndex >= xLength) return false;

            if (yIndex < 0 || yIndex >= yLength) return false;

            return true;
        }

        private GridBox CheckBox(int xIndex, int yIndex, int characterIndex)
        {
            if (CheckBounds(xIndex, yIndex))
            {
                GridBox targetBox = grids[xIndex, yIndex];

                if (!targetBox.IsOcupied) return null;
                    
                if(targetBox.currentCharacter.PlayerIndex != characterIndex)
                    return targetBox;
            }

            return null;
        }

        public GridBox CheckTargetsOnRange(GridBox origin, int characterIndex)
        {
            GridBox targetBox;

            //Left
            targetBox = CheckBox(origin.xIndex - 1, origin.yIndex, characterIndex);
            if (targetBox != null) return targetBox;

            //Right
            targetBox = CheckBox(origin.xIndex + 1, origin.yIndex, characterIndex);
            if (targetBox != null) return targetBox;

            //Up
            targetBox = CheckBox(origin.xIndex, origin.yIndex - 1, characterIndex);
            if (targetBox != null) return targetBox;

            //Down
            targetBox = CheckBox(origin.xIndex, origin.yIndex + 1, characterIndex);
            if (targetBox != null) return targetBox;

            return null;
        }

        public GridBox FindTarget(GridBox originBox, int characterIndex)
        {
            GridBox targetBox = null;
            int currentDistance = 0;

            for(int i = 0; i < xLength; i++)
            {
                for(int j = 0; j < yLength; j++)
                {
                    // Finded an player on a diferent team from target
                    if(grids[i, j].IsOcupied)
                    {
                        if (grids[i, j].currentCharacter.PlayerIndex == characterIndex) continue;

                        if(targetBox == null)
                        {
                            targetBox = grids[i, j];
                            currentDistance = GetDistance(originBox, targetBox);
                            continue;
                        }

                        int newDistance = GetDistance(originBox, grids[i, j]);
                        if(currentDistance > newDistance)
                        {
                            targetBox = grids[i, j];
                            currentDistance = newDistance;
                        }
                    }
                }
            }


            return targetBox;
        }

        private int GetDistance(GridBox origin, GridBox target)
        {
            int distanceX = Math.Abs(origin.xIndex - target.xIndex);
            int distanceY = Math.Abs(origin.yIndex - target.yIndex);

            return distanceX + distanceY;
        }

    }
}
