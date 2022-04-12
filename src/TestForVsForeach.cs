using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CSharpTestProject
{
    public class TestForVsForeach : IStartable
    {
        const int Size = 1000000;
        const int Iterations = 1000;

        public void Start()
        {
            var data = new List<double>();
            var rand = new Random();

            for (int i = 0; i < Size; i++)
            {
                data.Add(rand.NextDouble());
            }

            double correctSum = data.Sum();

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < Iterations; i++)
            {
                double sum = 0;
                for (int j = 0; j < data.Count; j++)
                {
                    sum += data[j];
                }
                if (Math.Abs(sum - correctSum) > 0.1)
                {
                    Console.WriteLine("Summation failed");
                    return;
                }
            }
            sw.Stop();
            Console.WriteLine($"For loop: {sw.ElapsedMilliseconds}");

            sw = Stopwatch.StartNew();
            for (int i = 0; i < Iterations; i++)
            {
                double sum = 0;
                foreach (double d in data)
                {
                    sum += d;
                }
                if (Math.Abs(sum - correctSum) > 0.1)
                {
                    Console.WriteLine("Summation failed");
                    return;
                }
            }
            sw.Stop();
            Console.WriteLine($"Foreach loop: {sw.ElapsedMilliseconds}");
        }
    } //Not writen by me
}
