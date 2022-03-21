using System;
using System.Collections.Generic;

namespace CSharpTestProject.FifteenPuzzle;
using Dir = Board.Direction;

internal class Tester : IStartable
{
    public void Start()
    {
        var matrix1 = new int[4][] {
            new int[] { 0, 1, 2, 3},
            new int[] { 4, 5, 6, 7},
            new int[] { 8, 9, 10, 11},
            new int[] { 12, 13, 14, 15},
        };

        var matrix2 = new int[4][] {
            new int[] { 1, 2, 3, 4},
            new int[] { 5, 6, 7, 8},
            new int[] { 9, 10, 0, 11},
            new int[] { 12, 13, 14, 15},
        };

        /*var matrix3 = new int[5][] {
            new int[] { 0, 1, 2, 3},
            new int[] { 4, 5, 6, 7},
            new int[] { 8, 12, 10, 11},
            new int[] { 12, 13, 14, 15},
            new int[] { 12, 13, 14, 15, 16, 17, 18},
        };*/

        var board1 = new Board(matrix1);
        var board2 = new Board(matrix2);

        Console.WriteLine($"board1.Hash = {board1.Hash}");
        Console.WriteLine($"board2.Hash = {board2.Hash}");
        Console.WriteLine();

        var rand = Random.Shared;
        int samesis = 0;
        int collisions = 0;
        var collidedBoards = new List<Board>(5);
        var prevHashes = new HashSet<ulong>();
        var prevBoards = new HashSet<Board>(new Board.Comparer());
        var prevBoard = new Board(matrix1);

        for (int i = 0; i < 10_000_000; i++)
        {
            Dir dir = (Dir)rand.Next(4);
            var board = prevBoard.Move(dir);
            ulong hash = board.Hash;

            bool boardPresent = !prevBoards.Add(board);
            if (boardPresent) samesis++;
            else
            {
                bool hashPresent = !prevHashes.Add(hash);
                if (hashPresent)
                {
                    collisions++;
                    collidedBoards.Add(board);
                }
            }

            prevBoard = board;
        }

        Console.WriteLine($"prevHashes.Count = {prevHashes.Count}, prevBoards.Count = {prevBoards.Count}");
        Console.WriteLine($"Samesis = {samesis}, collisions = {collisions}");

        foreach (var board in collidedBoards)
        {
            Console.WriteLine($"board.Hash = {board.Hash}");
            board.Print();
            Console.WriteLine();
        }


        Console.ReadKey();
    }
}
