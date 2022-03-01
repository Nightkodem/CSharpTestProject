using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CSharpTestProject
{
    public class Human
    {
        public string Name
        {
            get => _name;
            set
            {
                if (String.IsNullOrEmpty(value)) return;
                _name = value;
            }
        }
        public string Surname { get; set; }
        public string PersonalID { get; init; }

        private string _name;
        public Human()
        {
            Console.WriteLine("I'm alive!!!!!!11!1!!111!");
        }

        public Human(string name, string surname, string personalID)
            : this()
        {
            this.Name = name;
            this.Surname = surname;
            this.PersonalID = personalID;
        }

        public string GetHumanInfo()
        {
            return $"Imie = {Name}, nazwisko = {Surname}  (id = {PersonalID})";
        }

        public static int HumanMain()
        {
            var czlek1 = new Human("Marek", "Hucz", "1234");
            var czlek2 = new Human
            {
                Name = "Janek",
                Surname = "Hucz",
                PersonalID = "4321"
            };
            Console.WriteLine(czlek1.GetHumanInfo());

            czlek1.Name = "Rafał";
            czlek1.Surname = "Nierafał";

            Console.WriteLine(czlek1.GetHumanInfo());

            Console.ReadKey();
            return 0;
        }
    }
    public static class ZeroDivision
    {
        public static int ZeroDivisionMain()
        {
            const float x = 10f;
            const float zero = 0f;
            float result = x / zero;
            int result2 = 3;

            try
            {
                Math.DivRem((int)x, (int)zero, out result2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine($"Result2 = {result2},  10f/0f = {10f / 0f}");

            var v = new { Amount = 108, Message = "Hello" };

            Console.WriteLine($"v: {v}, \nType: {v.GetType()}, \nHash: {v.GetHashCode()}");

            Console.ReadKey();
            return 0;
        }
    }
    public static class GetLineFromString
    {
        public static int GetLineFromStringMain()
        {
            string content = File.ReadAllText(@"C:\Users\nikod\Temp\CShartProject\TextFile.txt");

            const uint line = 8;
            Console.WriteLine($"(Alg 1) Linijka {line}: {GetLineFromString.GetLine1(content, line, out long time1)}");
            Console.WriteLine($"(Alg 2) Linijka {line}: {GetLineFromString.GetLine2(content, line, out long time2)}");
            Console.WriteLine($"(Alg 3) Linijka {line}: {GetLineFromString.GetLine3(content, line, out long time3)}");

            Console.WriteLine($"Alg 1 czas: {time1}");
            Console.WriteLine($"Alg 2 czas: {time2}");
            Console.WriteLine($"Alg 3 czas: {time3}");


            Console.ReadKey();
            return 0;
        }

        /// <summary>
        /// Returns string containing a specified line <1, 2, ...> from whole string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lineNumber"></param>
        /// <returns name="line"></returns>
        public static string GetLine1(in string text, uint lineNumber, out long time)
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
        public static string GetLine2(string text, uint lineNumber, out long time)
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
        public static string GetLine3(string text, uint lineNumber, out long time)
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
    public static class DownloadTest
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


        public static void DownloadTestMain()
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

        private static string GetHumanReadableSize(ulong size)
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
    public static class ArrayArithetic
    {
        public const int MAX = 100000;

        public static int LongMultiply(int x, byte[] res, int res_size)
        {
            int carry = 0;

            for (int i = 0; i < res_size; ++i)
            {
                int prod = res[i] * x + carry;
                res[i] = (byte)(prod % 10);
                carry = prod / 10;
            }

            while (carry > 0)
            {
                res[res_size] = (byte)(carry % 10);
                carry /= 10;
                ++res_size;
            }

            return res_size;
        }

        public static void LongPow(int x, int n)
        {
            if (n == 0)
            {
                Console.Write("1");
                return;
            }

            byte[] res = new byte[MAX];
            int res_size = 0;
            int temp = x;

            while (temp != 0)
            {
                res[res_size++] = (byte)(temp % 10);
                temp /= 10;
            }

            for (int i = 2; i <= n; i++) res_size = LongMultiply(x, res, res_size);

            Console.Write($"{x}^{n} = "); for (int i = res_size - 1; i >= 0; i--) Console.Write(res[i]);
        }

        public static int ArrayAritheticMain()
        {
            ArrayArithetic.LongPow(7, 777);

            Console.ReadKey();
            return 0;
        }
    } //Not writen by me
    public static class StringToTitle
    {
        public static string ToTitle(string oldName)
        {
            string name = String.Empty;
            foreach (string w in oldName.Split('_'))
            {
                name += Convert.ToString(w[0]).ToUpper() + w.Substring(1) + " ";
            }

            return name.Trim();
        }

        public static int StringToTitleMain()
        {
            string oldName = "Lost_legacy_lala";
            string newName = StringToTitle.ToTitle(oldName);

            Console.WriteLine($"Name = {newName}");

            return 0;
        }
    }
    public static class SoundsExtractor
    {
        public static string[] ExtractSounds(string line)
        {
            string[] sounds = line.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (byte i = 0; i < sounds.Length; ++i)
            {
                sounds[i] = sounds[i].Trim();
            }

            return sounds;
        }

        public static int SoundsExtractorMain()
        {
            string data1 = "Wind Light, Rain Medium, Waterfall, Fireplace";
            string data2 = "Scream,,";
            string data3 = "";

            string[] sounds1 = SoundsExtractor.ExtractSounds(data1);
            string[] sounds2 = SoundsExtractor.ExtractSounds(data2);
            string[] sounds3 = SoundsExtractor.ExtractSounds(data3);

            Console.Write($"Sounds in data1: "); foreach (string s in sounds1) { Console.Write($"\"{s}\" "); }
            Console.WriteLine();
            Console.Write($"Sounds in data2: "); foreach (string s in sounds2) { Console.Write($"\"{s}\" "); }
            Console.WriteLine();
            Console.Write($"Sounds in data3: "); foreach (string s in sounds3) { Console.Write($"\"{s}\" "); }
            Console.WriteLine();

            Console.ReadKey();
            return 0;
        }
    }
    public static class GetAnimAndTextureName
    {
        public static (string tex, byte ver) GetTextureNamenadVersion(string name)
        {
            int braceIndex = name.IndexOf("(");

            if (braceIndex < 0) return (GetProperName(name), 0);

            string properNmae = GetProperName(name.Substring(0, braceIndex));
            byte version = Convert.ToByte(name.Substring(braceIndex + 1, name.IndexOf(")") - braceIndex - 1));

            return (properNmae, version);
        }

        public static string GetProperName(string name)
        {
            const char SPACEBAR = (char)32;
            string newName = String.Empty;

            foreach (string word in name.Split(new char[1] { SPACEBAR }, StringSplitOptions.RemoveEmptyEntries))
            {
                newName += word;
            }

            return newName;
        }

        public static int GetAnimAndTextureNameMain()
        {
            string anim1 = "Forge";
            string anim2 = "Royal Chamber";
            string tex1 = "Countryside";
            string tex2 = "Woods(1)";
            string tex3 = "Royal Chamber(13)";

            Console.WriteLine($"Anim \"{anim1}\" become \"{GetProperName(anim1)}\"");
            Console.WriteLine($"Anim \"{anim2}\" become \"{GetProperName(anim2)}\"");
            var (tex, ver) = ((string)null, (byte)0);
            (tex, ver) = GetTextureNamenadVersion(tex1); Console.WriteLine($"Tex \"{tex1}\" become \"{tex}\", ver {ver}");
            (tex, ver) = GetTextureNamenadVersion(tex2); Console.WriteLine($"Tex \"{tex2}\" become \"{tex}\", ver {ver}");
            (tex, ver) = GetTextureNamenadVersion(tex3); Console.WriteLine($"Tex \"{tex3}\" become \"{tex}\", ver {ver}");

            Console.ReadKey();
            return 0;
        }
    }
    public static class PlayerPosition
    {
        private static string path;
        private static int method;

        private const string FILE_NAME = "Noitisop.txt";

        public static void PlayerPositionMain()
        {
            PlayerPosition.Start();

            Console.WriteLine(PlayerPosition.ReadCurrentPosition());
            Console.WriteLine(PlayerPosition.ReadPrecedingAnimation());
            Console.WriteLine(PlayerPosition.ReadBackgroundPicture());
            Console.WriteLine(PlayerPosition.ReadBackgroundMusic());
            Console.WriteLine(PlayerPosition.ReadBackgroundSounds());

            Console.WriteLine($"Path: {path}");
        }

        public static void Start()
        {
            path = $"{AppContext.BaseDirectory}/{FILE_NAME}";
        }

        public static string ReadCurrentPosition()
        {
            return ReadInfo(InfoType.CurrentPosition);
        }

        public static string ReadPrecedingAnimation()
        {
            return ReadInfo(InfoType.PrecedingAnimation);
        }

        public static string ReadBackgroundPicture()
        {
            return ReadInfo(InfoType.BackgroundPicture);
        }

        public static string ReadBackgroundMusic()
        {
            return ReadInfo(InfoType.BackgroundMusic);
        }

        public static string ReadBackgroundSounds()
        {
            return ReadInfo(InfoType.BackgroundSounds);
        }

        public static void WriteCurrentPosition(string info)
        {
            WriteInfo(InfoType.CurrentPosition, info);
        }

        public static void WritePrecedingAnimation(string info)
        {
            WriteInfo(InfoType.PrecedingAnimation, info);
        }

        public static void WriteBackgroundPicture(string info)
        {
            WriteInfo(InfoType.BackgroundPicture, info);
        }

        public static void WriteBackgroundMusic(string info)
        {
            WriteInfo(InfoType.BackgroundMusic, info);
        }

        public static void WriteBackgroundSounds(string info)
        {
            WriteInfo(InfoType.BackgroundSounds, info);
        }


        private static string ReadInfo(InfoType infoType)
        {
            if (!CheckFile()) CreateFile();
            return File.ReadAllLines(path)[(byte)infoType].Trim();
        }

        private static void WriteInfo(InfoType infoType, string info)
        {
            if (!CheckFile()) CreateFile();
            if (info is null || info == String.Empty) return;
            string[] fileData = File.ReadAllLines(path);
            fileData[(byte)infoType] = info.Trim();
            File.WriteAllLines(path, fileData);
        }

        public static void CreateFile()
        {
            if (!CheckFile())
            {
                File.WriteAllBytes(path, new byte[0]);
                method = 1;
            }
            if (!CheckFile())
            {
                File.Create(path);
                method = 2;
            }
            if (!CheckFile())
            {
                using (var fstream = File.OpenWrite(path))
                {
                    fstream.Write(new byte[0], 0, 1);
                    fstream.Close();
                }
                method = 3;
            }
            if (!CheckFile())
            {
                using (var sw = File.AppendText(path))
                {
                    sw.Write(String.Empty);
                    sw.Close();
                }
                method = 4;
            }

            File.WriteAllLines(path, GetDefaults());
        }

        public static void DeleteFile()
        {
            if (CheckFile()) File.Delete(path);
        }

        public static bool CheckFile()
        {
            return File.Exists(path);
        }

        public static string GetPath()
        {
            return $"{path} m.{method}";
        }

        private static string[] GetDefaults()
        {
            string[] defaults = new string[5];

            defaults[(byte)InfoType.CurrentPosition] = "01/01/01/01/00/00/00/00/1";
            defaults[(byte)InfoType.PrecedingAnimation] = "Countryside";
            defaults[(byte)InfoType.BackgroundPicture] = "Countryside";
            defaults[(byte)InfoType.BackgroundMusic] = "Contemplating Flute";
            defaults[(byte)InfoType.BackgroundSounds] = "";

            return defaults;
        }

        private enum InfoType : byte
        {
            CurrentPosition = 0,
            PrecedingAnimation = 1,
            BackgroundPicture = 2,
            BackgroundMusic = 3,
            BackgroundSounds = 4
        }
    }
    public static class TestForVsForeach
    {
        const int Size = 1000000;
        const int Iterations = 1000;

        public static void TestForVsForeachMain()
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
    public static class HashOfStringValue
    {
        public static void GetHashOfStringValueMain()
        {
            string text1 = "jakis tekst";
            string text2 = "jakis inny tekst";
            string text3 = "jakis tekst";

            Console.WriteLine($"tekst1 = {text1}\t hash = {GetHashOfStringValue(text1)}");
            Console.WriteLine($"tekst1 = {text1}\t hash = {GetHashOfStringValue(text1)}");
            Console.WriteLine($"tekst2 = {text2}\t hash = {GetHashOfStringValue(text2)}");
            Console.WriteLine($"tekst3 = {text3}\t hash = {GetHashOfStringValue(text3)}");
        }

        private static string GetHashOfStringValue(string text)
        {
            string hashValueString = String.Empty;

            try
            {
                using var hash = MD5.Create();

                byte[] stringValue = Encoding.UTF8.GetBytes(text.Trim());
                byte[] hashValueBytes = hash.ComputeHash(stringValue);
                hashValueString = BitConverter.ToString(hashValueBytes).Replace("-", String.Empty).Trim().ToUpper();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                hashValueString = "Error";
            }

            return hashValueString;
        }
    }
    public static class UnlockedGraphicsTest
    {
        private const string DATA_DIRECTORY = @"C:\Users\nikod\Temp\CShartProject";
        private const string FILE_NAME = "Seciohc.llfile";
        private const uint ALLOWED_CHOICES = 2;
        private const uint FILE_SIZE = (byte)31;
        //choices

        private static string path;
        private static int method;
        private static bool started = false;

        public static void UnlockedGraphicsTestMain()
        {
            Start();
            DeleteFile();
            CreateFile();
            string unlockedGraphics = GetUnlockedGraphics();
            Console.WriteLine($"Unlocked before graphics = {unlockedGraphics}, wchich is {Convert.ToUInt32(unlockedGraphics, 2)}");

            AddUnlockedGraphic(GraphicName.RoyalChamber);
            AddUnlockedGraphic(GraphicName.LostLegacy);

            unlockedGraphics = GetUnlockedGraphics();
            Console.WriteLine($"Unlocked after graphics = {unlockedGraphics}, wchich is {Convert.ToUInt32(unlockedGraphics, 2)}");
        }

        public static void Start()
        {
            if (started) return;

            path = Path.Combine(DATA_DIRECTORY, FILE_NAME);

            started = true;
        }

        public static string GetUnlockedGraphics()
        {
            const int MAX_NUMBER = (int)GraphicName.LostLegacy;
            uint unlockeduint = Get(Field.UnlockedPictures);
            string unlockedString = Convert.ToString(unlockeduint, 2);

            var fullUnlockedString = new StringBuilder(MAX_NUMBER);

            int missingZeros = MAX_NUMBER - unlockedString.Length;
            for (int i = 0; i < missingZeros; ++i)
            {
                fullUnlockedString.Append("0");
            }
            fullUnlockedString.Append(unlockedString);

            return fullUnlockedString.ToString();
        }

        public static void AddUnlockedGraphic(GraphicName graphic)
        {
            string unlocked = GetUnlockedGraphics();
            int graphicNumber = (int)graphic - 1;
            var newUnlocked = new StringBuilder((int)GraphicName.LostLegacy);

            for (int i = 0; i < unlocked.Length; ++i)
            {
                if (i == graphicNumber) newUnlocked.Append("1");
                else newUnlocked.Append(unlocked[i]);
            }

            uint newValue = Convert.ToUInt32(newUnlocked.ToString(), 2);
            Set(Field.UnlockedPictures, newValue);
        }

        public static void AddAllowedMove()
        {
            uint moves = GetAllowedMoves();
            Set(Field.AllowedMoves, ++moves);
        }

        private static uint GetAllowedMoves()
        {
            return Get(Field.AllowedMoves);
        }

        public static bool IsMoveAllowed()
        {
            return GetAllowedMoves() < ALLOWED_CHOICES;
        }

        public static void Set(Field field, uint value)
        {
            if (!CheckFile()) CreateFile();

            string[] Lines = File.ReadAllLines(path);
            Lines[(uint)field - 1] = value.ToString();
            File.WriteAllLines(path, Lines);
        }

        public static uint Get(Field field)
        {
            if (!CheckFile()) return 1;

            int data = Convert.ToInt32(File.ReadAllLines(path)[(byte)field - 1]);
            return data < 0 ? (uint)1 : (uint)data;
        }

        public static void CreateFile()
        {
            if (!CheckFile())
            {
                File.WriteAllBytes(path, new byte[0]);
                method = 1;
            }
            if (!CheckFile())
            {
                File.Create(path);
                method = 2;
            }
            if (!CheckFile())
            {
                using (var fstream = File.OpenWrite(path))
                {
                    fstream.Write(new byte[0], 0, 1);
                    fstream.Close();
                }
                method = 3;
            }
            if (!CheckFile())
            {
                using (var sw = File.AppendText(path))
                {
                    sw.Write(String.Empty);
                    sw.Close();
                }
                method = 4;
            }
            File.WriteAllLines(path, GetDefaultFile());
        }

        private static string[] GetDefaultFile()
        {
            string[] newLines = new string[FILE_SIZE];

            for (byte i = 0; i < FILE_SIZE - 2; ++i)
            {
                newLines[i] = "1";
            }
            newLines[(int)Field.AllowedMoves - 1] = "0";
            newLines[(int)Field.UnlockedPictures - 1] = "0";

            return newLines;
        }

        public static void DeleteFile()
        {
            if (CheckFile()) File.Delete(path);
        }

        public static bool CheckFile()
        {
            return File.Exists(path);
        }

        public static string GetPath()
        {
            return $"{path} m.{method}";
        }

        public enum Field : uint
        {
            Choice1 = 1,
            Choice2 = 2,
            Choice3 = 3,
            Choice4 = 4,
            Choice5 = 5,
            Choice6 = 6,
            Choice7 = 7,
            Choice8 = 8,
            Choice9 = 9,
            Choice10 = 10,
            Choice11 = 11,
            Choice12 = 12,
            Choice13 = 13,
            Choice14 = 14,
            Choice15 = 15,
            Choice16 = 16,
            Choice17 = 17,
            Choice18 = 18,
            Choice19 = 19,
            Choice20 = 20,
            Choice21 = 21,
            Choice22 = 22,
            Choice23 = 23,
            Choice24 = 24,
            Choice25 = 25,
            Choice26 = 26,
            Choice27 = 27,
            Choice28 = 28,
            Choice29 = 29,
            AllowedMoves = 30,
            UnlockedPictures = 31
        }
        public enum GraphicName : uint
        {
            Countryside = 1,
            Inn = 2,
            Forge = 3,
            Market = 4,
            Shrine = 5,
            Harbor = 6,
            Whorehouse = 7,
            Riverport = 8,
            Merchants = 9,
            Underworld = 10,
            Suburbia = 11,
            Province = 12,
            Churchyard = 13,
            Dungeons = 14,
            Woods = 15,
            Forest = 16,
            Ballroom = 17,
            RoyalChamber = 18,
            LostLegacy = 19
        }
    }
    public static class BabiesBirthdayProblem
    {
        public static void BabiesBirthdayProblemMain()
        {
            const int DAYS = 365;
            const double INVERSE_DAYS = 1.0 / DAYS;

            double partialChance = 100.0;
            for (int i = 0; i < DAYS; ++i)
            {
                partialChance *= (DAYS - i) * INVERSE_DAYS;
                Console.WriteLine($"{i + 1}\tbabies have {(100.0 - partialChance):n6}% chance of sharing the same birthday");
            }
        }
    }
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
            for(int i = 1; i <= arr.Length; i++)
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
    public static class FckngListNodes
    {
        const int MAX = 10000;

        private class ListNode<T>
        {
            public T value { get; set; }
            public ListNode<T> next { get; set; }
        }

        private static class IntLinkedListCreator
        {
            public static ListNode<int> CreateListNode(string text)
            {
                int indexOfStart = text.IndexOf('[');
                int indexOfEnd = text.IndexOf(']');
                if (indexOfStart == -1 || indexOfEnd == -1 || indexOfStart >= indexOfEnd)
                    throw new ArgumentException($"Variable {nameof(text)} does not start with \"[\" or doesn not ends with \"]\"");

                string substring = text.Substring(indexOfStart + 1, indexOfEnd - 1) + ",";

                if (String.IsNullOrEmpty(substring) || substring == ",") return new ListNode<int>();

                ListNode<int> head = null;
                ListNode<int> current = null;

                var subPart = new StringBuilder(4);

                foreach (var c in substring)
                {
                    if (c != ',')
                    {
                        if (!Char.IsWhiteSpace(c)) subPart.Append(c);
                    }
                    else
                    {
                        int parsedValue = Int32.Parse(subPart.ToString().Trim());
                        subPart.Clear();

                        if (head is null)
                        {
                            head = new ListNode<int>();
                            current = head;
                            current.value = parsedValue;
                        }
                        else
                        {
                            var newNode = new ListNode<int>
                            {
                                value = parsedValue
                            };

                            current.next = newNode;
                            current = current.next;
                        }
                    }
                }

                if (head is null) head = new ListNode<int>();

                return head;
            }
        }

        private static void PrintLinkedList<T>(ListNode<T> head)
        {
            var sb = new StringBuilder("Linked List: ");
            var node = head;
            while (node is not null)
            {
                sb.Append(node.value).Append(", ");
                node = node.next;
            }
            sb.Remove(sb.Length - 2, 1);
            Console.WriteLine(sb.ToString());
        }

        public static void FckngListNodesMain()
        {
            ListNode<int> a = IntLinkedListCreator.CreateListNode("[1]");
            ListNode<int> b = IntLinkedListCreator.CreateListNode("[9999, 9999]");
            ListNode<int> l = IntLinkedListCreator.CreateListNode("[1, 2, 3, 4, 5, 6, 7]");

            PrintLinkedList(S_RearrangeLastN(l, 6));
        }

        private static ListNode<int> S_Reverse_WithoutColections(ListNode<int> l)
        {
            ListNode<int> curr = l;
            ListNode<int> prev = null;

            while (curr is not null)
            {
                var next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }

            return prev;
        }

        private static ListNode<int> S_Revers_WithStack(ListNode<int> l)
        {
            if (l is null) return null;
            if (l.next is null) return l;

            Stack<ListNode<int>> listStack = new Stack<ListNode<int>>();

            for (var n = l; n is not null; n = n.next)
            {
                listStack.Push(n);
            }

            ListNode<int> head = null;
            ListNode<int> prev = null;

            int count = listStack.Count;

            for (int i = 0; i < count; i++)
            {
                if (head is null)
                {
                    head = listStack.Pop();
                    prev = head;
                }
                else
                {
                    var n = listStack.Pop();
                    prev.next = n;
                    prev = prev.next;
                }
            }
            prev.next = null;

            return head;
        }

        private static ListNode<int> S_Reverse_WithList(ListNode<int> l)
        {
            if (l is null) return null;

            ListNode<int> newHead = null;

            List<ListNode<int>> listList = new List<ListNode<int>>();

            for (ListNode<int> n = l; n != null; n = n.next)
            {
                listList.Add(n);
            }

            int count = listList.Count;
            newHead = listList[count - 1];

            for (int i = count - 1; i >= 1; i--)
            {
                listList[i].next = listList[i - 1];
            }
            listList[0].next = null;

            return newHead;
        }

        private static ListNode<int> S_RearrangeLastN(ListNode<int> l, int n)
        {
            if (n <= 0) return l;

            var head = l;

            int count = 0;
            for (var curr = l; curr != null; curr = curr.next)
            {
                count++;
            }

            if (count <= n) return l;

            int i = 0;
            for (var curr = l; curr != null;)
            {
                var tmpNext = curr.next;

                if (i == count - n - 1)
                {
                    curr.next = null;
                    head = tmpNext;
                }
                else if (i == count - 1)
                {
                    curr.next = l;
                }

                curr = tmpNext;
                i++;
            }

            return head;
        }
        
        private static ListNode<int> S_ReverseGroupsOfK(ListNode<int> l, int k)
        {
            if (k <= 1) return l;

            ListNode<int>[] buff = new ListNode<int>[k];
            ListNode<int> head = null;
            ListNode<int> prev = null;

            int count = 0;
            for (var n = l; n != null;)
            {
                buff[count] = n;
                n = n.next;

                if (count == k - 1)
                {
                    if (head is null) head = buff[k - 1];
                    if (prev is not null) prev.next = buff[k - 1];

                    for (int i = k - 1; i > 0; i--)
                    {
                        buff[i].next = buff[i - 1];
                    }
                    prev = buff[0];
                    
                    count = 0;
                }
                else count++;
            }

            if (count > 0)
            {
                prev.next = buff[0];
                buff[count - 1].next = null;
            }
            else
            {
                prev.next = null;
            }

            return head;
        }

        private static ListNode<int> S_SumHugeNumber(ListNode<int> a, ListNode<int> b)
        {
            const int MAX = 10000;

            List<int> aList = new List<int>();
            List<int> bList = new List<int>();

            var aNode = a;
            var bNode = b;
            bool aIsNotNull = aNode != null;
            bool bIsNotNull = bNode != null;
            while (aIsNotNull || bIsNotNull)
            {
                if (aIsNotNull)
                {
                    aList.Add(aNode.value);
                    aNode = aNode.next;
                    aIsNotNull = aNode != null;
                }
                if (bIsNotNull)
                {
                    bList.Add(bNode.value);
                    bNode = bNode.next;
                    bIsNotNull = bNode != null;
                }
            }

            ListNode<int> result = null;

            int aLength = aList.Count;
            int bLength = bList.Count;
            int length = aLength >= bLength ? aLength : bLength;
            int overflow = 0;
            for (int i = 0; i < length; i++)
            {
                int aToAdd = 0;
                int bToAdd = 0;

                if (i < aLength)
                {
                    aToAdd = aList[aLength - i - 1];
                }
                if (i < bLength)
                {
                    bToAdd = bList[bLength - i - 1];
                }
                int sum = aToAdd + bToAdd + overflow;
                int toPut = sum % MAX;
                overflow = sum >= MAX ? 1 : 0;

                if (result is null)
                {
                    result = new ListNode<int>();
                    result.value = toPut;
                }
                else
                {
                    var newNode = new ListNode<int>
                    {
                        value = toPut
                    };

                    newNode.next = result;
                    result = newNode;
                }
            }

            if (overflow > 0)
            {
                var newNode = new ListNode<int>
                {
                    value = 1
                };

                newNode.next = result;
                result = newNode;
            }

            if (result is null) result = new ListNode<int>();

            return result;
        }
    }
    public static class Matrices
    {
        static int count = 0;
        public static void MatricesMain()
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

        private static int[][] RemoveIslands(int[][] m)
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

        private static void RemoveIsnaldsRecursive(int[][] m, int x, int y, bool[][] keep)
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

    public static class Sequences
    {
        public static void SequencesMain()
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

        private static bool IsStrictlyIncreasingShitty(int[] sequence)
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


        private static bool IsStrictlyIncreasing(int[] sequence)
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

        private static bool IsStrictlyIncreasingFailed(int[] sequence)
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

    public class Program
    {
        public static int Main()
        {
            Sequences.SequencesMain();

            Console.ReadKey();
            return 0;
        }
    }
}
