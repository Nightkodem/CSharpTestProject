using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CSharpTestProject;

public class AbsoluteValueTest : IStartable // Not written by me
{
    /*
        RESULTS .NET 6.0


        Method Math.Abs(): 3529

        Inline i > 0 ? i : -i: 3181
        Method AbsoluteTernary(): 6719
        Method AbsoluteTernaryInlining(): 6706

        Inline (i + (i >> 31)) ^ (i >> 31): 3129
        Method AbsoluteBitOp(): 5963
        Method AbsoluteBitOpInlining(): 5955
    */

    public static int x; // public static field. 
                         // this way the JITer will not assume that it is  
                         // never used and optimize the wholeloop away
    public void Start()
    {
        // warm up
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = Math.Abs(i);
        }

        // start measuring
        Stopwatch watch = Stopwatch.StartNew();
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = Math.Abs(i);
        }
        Console.WriteLine($"Method Math.Abs(): {watch.ElapsedMilliseconds}");



        // warm up
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = i > 0 ? i : -i;
        }

        // start measuring
        watch = Stopwatch.StartNew();
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = i > 0 ? i : -i;
        }
        Console.WriteLine($"Inline i > 0 ? i : -i: {watch.ElapsedMilliseconds}");


        // warm up
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = AbsoluteTernary(i);
        }

        // start measuring
        watch = Stopwatch.StartNew();
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = AbsoluteTernary(i);
        }
        Console.WriteLine($"Method AbsoluteTernary(): {watch.ElapsedMilliseconds}");


        // warm up
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = AbsoluteTernaryInlining(i);
        }

        // start measuring
        watch = Stopwatch.StartNew();
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = AbsoluteTernaryInlining(i);
        }
        Console.WriteLine($"Method AbsoluteTernaryInlining(): {watch.ElapsedMilliseconds}");



        // warm up
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = (i + (i >> 31)) ^ (i >> 31);
        }

        // start measuring
        watch = Stopwatch.StartNew();
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = (i + (i >> 31)) ^ (i >> 31);
        }
        Console.WriteLine($"Inline (i + (i >> 31)) ^ (i >> 31): {watch.ElapsedMilliseconds}");


        // warm up
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = AbsoluteBitOp(i);
        }

        // start measuring
        watch = Stopwatch.StartNew();
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = AbsoluteBitOp(i);
        }
        Console.WriteLine($"Method AbsoluteBitOp(): {watch.ElapsedMilliseconds}");


        // warm up
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = AbsoluteBitOpInlining(i);
        }

        // start measuring
        watch = Stopwatch.StartNew();
        for (int i = -1000000000; i < 1000000000; i++)
        {
            x = AbsoluteBitOpInlining(i);
        }
        Console.WriteLine($"Method AbsoluteBitOpInlining(): {watch.ElapsedMilliseconds}");



        Console.ReadLine();
    }


    private static int AbsoluteTernary(int i)
    {
        return i > 0 ? i : -i;
    }

    private static int AbsoluteBitOp(int i)
    {
        return (i + (i >> 31)) ^ (i >> 31);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int AbsoluteTernaryInlining(int i)
    {
        return i > 0 ? i : -i;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int AbsoluteBitOpInlining(int i)
    {
        return (i + (i >> 31)) ^ (i >> 31);
    }
}
