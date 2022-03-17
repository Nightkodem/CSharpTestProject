using System;

namespace CSharpTestProject
{
    public class Matrices : IStartable
    {
        static int count = 0;
        public void Start()
        {
            var matrix1 = new int[6][]
            {
                new int[] {1, 0, 0, 0, 0, 0},
                new int[] {0, 1, 0, 1, 1, 1},
                new int[] {0, 0, 1, 0, 1, 0},
                new int[] {1, 1, 0, 0, 1, 0},
                new int[] {1, 0, 1, 1, 0, 0},
                new int[] {1, 0, 0, 0, 0, 1}
            };

            RemoveIslands(matrix1).Print();
        }

        private int[][] RemoveIslands(int[][] m)
        {
            count = 0;
            int xLen = m.Length;
            int yLen = m[0].Length;

            bool[][] keep = new bool[xLen][];
            for (int i = 0; i < xLen; i++)
            {
                keep[i] = new bool[yLen];
            }

            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    if ((i <= 0 || i >= xLen - 1) ||
                        (j <= 0 || j >= yLen - 1))
                    {
                        RemoveIsnaldsRecursive(m, i, j, keep);
                    }
                }
            }

            for (int i = 1; i < xLen - 1; i++)
            {
                for (int j = 1; j < yLen - 1; j++)
                {
                    if (!keep[i][j]) m[i][j] = 0;
                }
            }

            Console.WriteLine($"Count = {count}");

            return m;
        }

        private void RemoveIsnaldsRecursive(int[][] m, int x, int y, bool[][] keep)
        {
            count++;
            if (m[x][y] == 1) keep[x][y] = true;
            else return;

            int xLen = m.Length;
            int yLen = m[0].Length;

            if (x >= 1 &&
                !keep[x - 1][y])
                RemoveIsnaldsRecursive(m, x - 1, y, keep);
            if (x <= xLen - 2 &&
                !keep[x + 1][y])
                RemoveIsnaldsRecursive(m, x + 1, y, keep);
            if (y >= 1 &&
                !keep[x][y - 1])
                RemoveIsnaldsRecursive(m, x, y - 1, keep);
            if (y <= yLen - 2 &&
                !keep[x][y + 1])
                RemoveIsnaldsRecursive(m, x, y + 1, keep);
        }
    }
}
