using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTestProject.FifteenPuzzle;

public static class MathI
{
    public static ulong Power(ulong n, int pow)
    {
        ulong result = 1;
        for (int i = 0; i < pow; i++)
        {
            result *= n;
        }
        return result;
    }
}
