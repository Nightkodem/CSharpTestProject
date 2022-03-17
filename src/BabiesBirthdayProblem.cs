using System;

namespace CSharpTestProject
{
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
}
