using System;
using System.IO;

namespace CSharpTestProject;

public class BytesDistribution : IStartable
{
    public void Start()
    {
        int[] bytes = new int[256];

        var data = File.ReadAllBytes(@"C:\Users\nikod\Temp\ToRemove\lostlegacy_githubrepocard.png");

        foreach (byte b in data)
        {
            bytes[b]++;
        }

        Array.Sort(bytes);

        for (int i = 0; i < bytes.Length; i++)
        {
            Console.WriteLine($"Byte {i} appeared {bytes[i]} times");
        }
    }
}
