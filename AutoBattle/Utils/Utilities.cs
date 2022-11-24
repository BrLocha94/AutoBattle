using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Utils
{
    public static class Utilities
    {
        static Random rand = new Random();

        public static void ShuffeList<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T temp = list[i];
                int randomIndex = rand.Next(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        public static int GetRandomInt(int min, int max)
        {
            int index = rand.Next(min, max);
            return index;
        }
    }
}
