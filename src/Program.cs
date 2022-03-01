using System;
using System.Diagnostics;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CSharpTestProject
{
    public interface IStartable
    {
        void Start();
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

    public class Human : IStartable
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

        private string GetHumanInfo()
        {
            return $"Imie = {Name}, nazwisko = {Surname}  (id = {PersonalID})";
        }

        public void Start()
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
        }
    }

    public class ZeroDivision : IStartable
    {
        public void Start()
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

            Console.WriteLine($"Result = {result}, Result2 = {result2},  10f/0f = {10f / 0f}");

            var v = new { Amount = 108, Message = "Hello" };

            Console.WriteLine($"v: {v}, \nType: {v.GetType()}, \nHash: {v.GetHashCode()}");
        }
    }

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

    public class ArrayArithetic : IStartable
    {
        private static readonly int MAX = 100000;

        private int LongMultiply(int x, byte[] res, int res_size)
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

        private void LongPow(int x, int n)
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

        public void Start()
        {
            LongPow(7, 777);
        }
    } //Not writen by me

    public class StringToTitle : IStartable
    {
        private string ToTitle(string oldName)
        {
            string name = String.Empty;
            foreach (string w in oldName.Split('_'))
            {
                name += Convert.ToString(w[0]).ToUpper() + w.Substring(1) + " ";
            }

            return name.Trim();
        }

        public void Start()
        {
            string oldName = "Lost_legacy_lala";
            string newName = ToTitle(oldName);

            Console.WriteLine($"Name = {newName}");
        }
    }

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

    public class GetAnimAndTextureName : IStartable
    {
        private (string tex, byte ver) GetTextureNamenadVersion(string name)
        {
            int braceIndex = name.IndexOf("(");

            if (braceIndex < 0) return (GetProperName(name), 0);

            string properNmae = GetProperName(name.Substring(0, braceIndex));
            byte version = Convert.ToByte(name.Substring(braceIndex + 1, name.IndexOf(")") - braceIndex - 1));

            return (properNmae, version);
        }

        private string GetProperName(string name)
        {
            const char SPACEBAR = (char)32;
            string newName = String.Empty;

            foreach (string word in name.Split(new char[1] { SPACEBAR }, StringSplitOptions.RemoveEmptyEntries))
            {
                newName += word;
            }

            return newName;
        }

        public void Start()
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
        }
    }

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

    public class HashOfStringValue : IStartable
    {
        public void Start()
        {
            string text1 = "jakis tekst";
            string text2 = "jakis inny tekst";
            string text3 = "jakis tekst";

            Console.WriteLine($"tekst1 = {text1}\t hash = {GetHashOfStringValue(text1)}");
            Console.WriteLine($"tekst1 = {text1}\t hash = {GetHashOfStringValue(text1)}");
            Console.WriteLine($"tekst2 = {text2}\t hash = {GetHashOfStringValue(text2)}");
            Console.WriteLine($"tekst3 = {text3}\t hash = {GetHashOfStringValue(text3)}");
        }

        private string GetHashOfStringValue(string text)
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

    public class BabiesBirthdayProblem : IStartable
    {
        public void Start()
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

    public class FckngListNodes : IStartable
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

        public void Start()
        {
            ListNode<int> a = IntLinkedListCreator.CreateListNode("[1]");
            ListNode<int> b = IntLinkedListCreator.CreateListNode("[9999, 9999]");
            ListNode<int> l = IntLinkedListCreator.CreateListNode("[1, 2, 3, 4, 5, 6, 7]");

            PrintLinkedList(S_RearrangeLastN(l, 6));
        }

        private ListNode<int> S_Reverse_WithoutColections(ListNode<int> l)
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

        private ListNode<int> S_Revers_WithStack(ListNode<int> l)
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

        private ListNode<int> S_Reverse_WithList(ListNode<int> l)
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

        private ListNode<int> S_RearrangeLastN(ListNode<int> l, int n)
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
        
        private ListNode<int> S_ReverseGroupsOfK(ListNode<int> l, int k)
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

        private ListNode<int> S_SumHugeNumber(ListNode<int> a, ListNode<int> b)
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

    public class Program
    {
        public static void Main()
        {
            IStartable startable = new ZeroDivision();
            startable.Start();

            Console.ReadKey();
        }
    }
}
