using System;

namespace CSharpTestProject
{
    public static class MyOwnShuffle
    {
        private static readonly Random rand = new();

        public static void MyOwnShuffleMain()
        {
            var stringArray = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

            PrintArrayIndexed(stringArray.Shuffle());
        }

        private static void PrintArrayIndexed<T>(T[] arr)
        {
            for (int i = 1; i <= arr.Length; i++)
            {
                Console.WriteLine($"{i}.\t{arr[i]}");
            }
        }

        private static T[] Shuffle<T>(this T[] arr)
        {
            int length = arr.Length;

            for (int i = 0; i < length; i++)
            {
                int toSwap = rand.Next(0, length - 1);
                T temp = arr[i];
                arr[i] = arr[toSwap];
                arr[toSwap] = temp;
            }

            return arr;
        }
    }
}
