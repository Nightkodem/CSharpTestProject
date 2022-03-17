using System;

namespace CSharpTestProject
{
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
}
