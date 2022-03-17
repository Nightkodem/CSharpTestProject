using System;

namespace CSharpTestProject
{
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
}
