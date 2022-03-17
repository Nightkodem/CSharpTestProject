using System;

namespace CSharpTestProject
{
    public class Sequences : IStartable
    {
        public void Start()
        {
            int[] seq1 = { 1, 2, 3, 4, 5, 6 };
            int[] seq2 = { 1, 2, 5, 3, 5 };
            int[] seq3 = { 1, 2, 1, 2 };
            int[] seq4 = { 10, 1, 2, 1, 2 };

            Console.WriteLine($"seq1: expected = {IsStrictlyIncreasingShitty(seq1)}, actual = {IsStrictlyIncreasing(seq1)}");
            Console.WriteLine($"seq2: expected = {IsStrictlyIncreasingShitty(seq2)}, actual = {IsStrictlyIncreasing(seq2)}");
            Console.WriteLine($"seq3: expected = {IsStrictlyIncreasingShitty(seq3)}, actual = {IsStrictlyIncreasing(seq3)}");
            Console.WriteLine($"seq4: expected = {IsStrictlyIncreasingShitty(seq4)}, actual = {IsStrictlyIncreasing(seq4)}");
        }

        private bool IsStrictlyIncreasingShitty(int[] sequence)
        {
            int length = sequence.Length;
            for (int i = 0; i < length; i++)
            {
                bool partialYes = true;
                for (int j = 0; j < length - 1; j++)
                {
                    if (j != i)
                    {
                        int next = j + 1 != i ? j + 1 : j + 2;
                        if (next >= length) break;
                        partialYes &= (sequence[j] < sequence[next]);
                    }
                }
                if (partialYes) return true;
            }
            return false;
        }


        private bool IsStrictlyIncreasing(int[] sequence)
        {
            const int MAX_SWAPS = 1;

            int length = sequence.Length;

            if (sequence is null || length <= 1) return true;

            int toRemove = 0;
            for (int i = 1; i < length; i++)
            {
                int value0 = sequence[i - 1];
                int value1 = sequence[i];

                if (value0 >= value1)
                {
                    toRemove++;
                    sequence[i] = value0;

                    if (toRemove > MAX_SWAPS) return false;
                }
            }

            return true;
        }

        private bool IsStrictlyIncreasingFailed(int[] sequence)
        {
            const int MAX_SWAPS = 1;

            int length = sequence.Length;

            if (sequence is null || length <= 1) return true;

            int toRemove = 0;

            int currMax = sequence[0];
            for (int i = 1; i < length; i++)
            {
                int value = sequence[i];
                if (value <= currMax)
                {
                    toRemove++;

                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (value <= sequence[j]) 
                        {
                            toRemove++;
                            break;
                        }
                    }

                    if (toRemove > MAX_SWAPS) return false;
                }
                else
                {
                    currMax = value;
                }
            }

            return true;
        }
    }
}
