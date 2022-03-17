using System;
using System.Diagnostics;
using System.Text;

namespace CSharpTestProject
{
    public class GetLineFromString : IStartable
    {
        public void Start()
        {
            string content = GetLongContent(100_000);

            const uint line = 500;
            Console.WriteLine($"(Alg 1) Linijka {line}: {GetLine1(content, line, out long time1)}");
            Console.WriteLine($"(Alg 2) Linijka {line}: {GetLine2(content, line, out long time2)}");
            Console.WriteLine($"(Alg 3) Linijka {line}: {GetLine3(content, line, out long time3)}");

            Console.WriteLine($"Alg 1 czas: {time1}");
            Console.WriteLine($"Alg 2 czas: {time2}");
            Console.WriteLine($"Alg 3 czas: {time3}");
        }

        private string GetLongContent(int length)
        {
            var sb = new StringBuilder(length);

            for (int i = 1; i <= length; i++)
            {
                sb.Append(i).Append('\n');
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns string containing a specified line <1, 2, ...> from whole string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lineNumber"></param>
        /// <returns name="line"></returns>
        private string GetLine1(in string text, uint lineNumber, out long time)
        {
            var watch = new Stopwatch();
            time = 0;

            watch.Start();
            if (lineNumber < 1) return String.Empty;

            int count = 1;
            int i;
            foreach (char c in text)
            {
                if (c == '\n') ++count;
            }

            if (lineNumber > count) return String.Empty;

            int[] indexesOfEnter = new int[count + 1];
            indexesOfEnter[0] = -1;

            count = 1;
            for (i = 0; i < text.Length; ++i)
            {
                if (text[i] == '\n')
                {
                    indexesOfEnter[count] = i;
                    ++count;
                }
            }
            indexesOfEnter[count] = text.Length;

            int startIndex = indexesOfEnter[lineNumber - 1] + 1;
            int lenght = indexesOfEnter[lineNumber] - startIndex;
            string line = text.Substring(startIndex, lenght);
            watch.Stop();

            time = watch.ElapsedMilliseconds;
            return line;
        }

        /// <summary>
        /// Returns string containing a specified line <1, 2, ...> from whole string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lineNumber"></param>
        /// <returns name="line"></returns>
        private string GetLine2(string text, uint lineNumber, out long time)
        {
            var watch = new Stopwatch();
            time = 0;

            watch.Start();
            if (lineNumber < 1) return String.Empty;

            string line = text;

            for (int i = 0; i < lineNumber; ++i)
            {
                int indexOfNewLine = text.IndexOf('\n');
                int textLenght = text.Length;
                bool hasNewLine = indexOfNewLine != -1;
                int indexOfEndline = (hasNewLine ? indexOfNewLine : textLenght);
                line = text.Substring(0, indexOfEndline);
                if (hasNewLine) text = text.Substring(indexOfEndline + 1, textLenght - line.Length - 1);
                else break;
            }
            watch.Stop();

            time = watch.ElapsedMilliseconds;
            return line;
        }

        /// <summary>
        /// Returns string containing a specified line <1, 2, ...> from whole string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lineNumber"></param>
        /// <returns name="line"></returns>
        private string GetLine3(string text, uint lineNumber, out long time)
        {
            var watch = new Stopwatch();
            time = 0;

            watch.Start();
            if (lineNumber < 1) return String.Empty;

            string[] textFileArray = text.Split('\n');

            if (lineNumber > textFileArray.Length) return String.Empty;
            watch.Stop();

            time = watch.ElapsedMilliseconds;
            return textFileArray[lineNumber - 1];
        }
    }
}
