using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpTestProject
{
    public static class Printer
    {
        public static void Print<T>(this T[] arr)
        {
            var sb = new StringBuilder(arr.Length);
            for (int i = 1; i <= arr.Length; i++)
            {
                sb.Append(arr[i]).Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            Console.WriteLine(sb.ToString());
        }

        public static void Print<T>(this ICollection<T> source)
        {
            int length = source.Count;
            var sb = new StringBuilder(length);
            foreach (T element in source)
            {
                sb.Append(element).Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            Console.WriteLine(sb.ToString());
        }

        public static void Print<T>(this T[][] matrix)
        {
            if (matrix is null) throw new ArgumentException("Matrix can not be null");

            int xLength = matrix.Length;

            if (xLength <= 0) throw new ArgumentException("Matrix can not be empty");

            int yLength = matrix[0].Length;

            for (int i = 0; i < xLength; i++)
            {
                if (matrix[i].Length != yLength) throw new ArgumentException("Inner arrays should have the same length");
            }

            string[][] values = new string[xLength][];

            int maxValLen = 0;
            for (int i = 0; i < xLength; i++)
            {
                values[i] = new string[yLength];

                for (int j = 0; j < yLength; j++)
                {
                    string valueToInsert = matrix[i][j].ToString();
                    int valueToInsertLen = valueToInsert.Length;

                    values[i][j] = valueToInsert;
                    if (valueToInsertLen > maxValLen) maxValLen = valueToInsertLen;
                }
            }

            var sb = new StringBuilder((xLength + 2) * (yLength + 2));

            int maxIndicesLen = (int)Math.Log10(xLength) + 3;
            sb.Append("".PadLeft(maxIndicesLen, ' '));
            for (int i = 0; i < yLength; i++)
            {
                string valueToInsert = (i + 1).ToString().PadLeft(maxValLen + 2);
                sb.Append(valueToInsert);
            }
            sb.Append('\n');
            for (int i = 0; i < xLength; i++)
            {
                sb.Append((i + 1).ToString().PadRight(maxIndicesLen)).Append('[');
                for (int j = 0; j < yLength; j++)
                {
                    string valueToInsert = values[i][j].PadLeft(maxValLen, ' ');
                    sb.Append(' ').Append(valueToInsert).Append(' ');
                }
                sb.Append("]\n");
            }

            Console.WriteLine(sb.ToString());
        }

        public static void PrintIndexed<T>(this T[] arr)
        {
            var sb = new StringBuilder(arr.Length);
            for (int i = 1; i <= arr.Length; i++)
            {
                sb.Append(i).Append(".\t").Append(arr[i]).Append('\n');
            }
            sb.Remove(sb.Length - 1, 1);
            Console.WriteLine(sb.ToString());
        }

        public static void PrintIndexed<T>(this ICollection<T> source)
        {
            int length = source.Count;
            var sb = new StringBuilder(length);
            int counter = 1;
            foreach (T element in source)
            {
                sb.Append(counter).Append(".\t").Append(element).Append('\n');
                counter++;
            }
            sb.Remove(sb.Length - 1, 1);
            Console.WriteLine(sb.ToString());
        }
    }
}
