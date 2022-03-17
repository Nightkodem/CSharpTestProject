using System;

namespace CSharpTestProject
{
    public class SoundsExtractor : IStartable
    {
        private string[] ExtractSounds(string line)
        {
            string[] sounds = line.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (byte i = 0; i < sounds.Length; ++i)
            {
                sounds[i] = sounds[i].Trim();
            }

            return sounds;
        }

        public void Start()
        {
            string data1 = "Wind Light, Rain Medium, Waterfall, Fireplace";
            string data2 = "Scream,,";
            string data3 = "";

            string[] sounds1 = ExtractSounds(data1);
            string[] sounds2 = ExtractSounds(data2);
            string[] sounds3 = ExtractSounds(data3);

            Console.Write($"Sounds in data1: "); foreach (string s in sounds1) { Console.Write($"\"{s}\" "); }
            Console.WriteLine();
            Console.Write($"Sounds in data2: "); foreach (string s in sounds2) { Console.Write($"\"{s}\" "); }
            Console.WriteLine();
            Console.Write($"Sounds in data3: "); foreach (string s in sounds3) { Console.Write($"\"{s}\" "); }
            Console.WriteLine();
        }
    }
}
