using System.Threading;
using System.Linq;
using System;

namespace CSharpTestProject;

public class DraftBook : IStartable
{
    private Singleton sing;

    public void Start()
    {
        byte[] data = Enumerable.Range(0, 128).Select(x => (byte)x).ToArray();

        Console.Write($"CRC = "); CRCNiemoje(data).Print();
        Console.Write($"Moje CRC = "); CRC(data).Print();
    }

    public static byte[] CRC(byte[] data)
    {
        const int Polynomial = 0x1021;
        const int Bits = 8;

        //int offset = 3;

        int crc = 0;
        for (var i = 0; i < 128; i++)
        {
            crc ^= data[i] << Bits;
            for (var j = 0; j < Bits; j++)
            {
                if ((crc & 0x8000) != 0) crc = (crc << 1) ^ Polynomial;
                else crc <<= 1;
            }
        }
        return new[] { (byte)(crc >> 8), (byte)crc };
    }

    public static byte[] CRCNiemoje(byte[] packet)
    {
        int crc;
        int i;
        int j = 128;
        crc = 0;
        int k = 0;
        while (--j >= 0)
        {
            crc = crc ^ (int)packet[k++] << 8;
            i = 8;
            do
            {
                if ((crc & 0x8000) != 0)
                    crc = crc << 1 ^ 0x1021;
                else
                    crc = crc << 1;
            } while (--i > 0);
        }
        return new byte[] { (byte)(crc >> 8), (byte)crc };
    }

    public void CreateInstance()
    {
        System.Console.WriteLine("Creating Instance");
        sing = Singleton.Instance;
    }

    class Singleton
    {
        public static int instances = 0;
        public static int getterCalls = 0;

        public static Singleton Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new Singleton();
                }
                getterCalls++;
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        private static Singleton _instance = null;

        public Singleton()
        {
            instances++;
        }

        public void DoWork()
        {
            int a = 3;
            int b = 4;
            int sum = a + b;
        }
    }
}
