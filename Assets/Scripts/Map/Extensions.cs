using System.Collections;
using System.Collections.Generic;

namespace A
{
    public static class Extensions
    {
        private static System.Random randNum = new System.Random();

        public static void Shuffle(this IList list)
        {
            for (int n = list.Count - 1; n > 0; n--)
            {
                int k = randNum.Next(n);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}
