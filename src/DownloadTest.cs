using System;
using System.Collections.Generic;
using System.Net;

namespace CSharpTestProject
{
    public class DownloadTest : IStartable
    {
        private static readonly Dictionary<ulong, string> metricPrefixes = new Dictionary<ulong, string>
            {
                { 1, "B" },
                { 1_000, "kB" },
                { 1_000_000, "MB" },
                { 1_000_000_000, "GB" },
                { 1_000_000_000_000, "TB" },
                { 1_000_000_000_000_000, "PB" }
            };

        private static readonly string[] TEST_URLS = new string[]
            {
                    @"https://example.com/",
                    @"https://example.org/",
                    //@"https://play.google.com/store",
                    @"https://drive.google.com/uc?export=download&id=1P8gf5-DEvSYpFhGDylD6u5yiLXfY8B7T",
                    @"https://drive.google.com/uc?export=download&id=17Y-obCuaHYbP3dtjkiLYO66SB8UjFQ3f",
                    @"https://www.blank.org/"
            };


        public void Start()
        {
            var datas = new byte[TEST_URLS.Length][];

            using (var client = new WebClient())
            {
                for (int i = 0; i < TEST_URLS.Length; i++)
                {
                    datas[i] = client.DownloadData(TEST_URLS[i]);
                    Console.WriteLine($"{TEST_URLS[i]} done");
                }
            }

            for (int i = 0; i < TEST_URLS.Length; i++)
            {
                Console.WriteLine($"Size of site {TEST_URLS[i]} = {GetHumanReadableSize((ulong)datas[i].Length)}  ({datas[i].Length})");
            }
        }

        private string GetHumanReadableSize(ulong size)
        {
            foreach (var k in metricPrefixes.Keys)
            {
                if (size < k * 1000)
                {
                    string prefix = metricPrefixes[k];
                    if (prefix == "B")
                    {
                        return $"{((double)size / k):n0}{prefix}";
                    }
                    else
                    {
                        return $"{((double)size / k):n2}{prefix}";
                    }
                }
            }
            return $"{size}B";
        }
    }
}
