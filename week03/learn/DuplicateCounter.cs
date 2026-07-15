using System;
using System.Collections.Generic;

public static class DuplicateCounter
{
    /// <summary>
    /// Counts the number of duplicates in a list of integers.
    /// </summary>
    public static int CountDuplicates(List<int> numbers)
    {
        var uniqueNumbers = new HashSet<int>();
        int duplicateCount = 0;

        foreach (var number in numbers)
        {
            if (!uniqueNumbers.Add(number))
            {
                duplicateCount++;
            }
        }

        return duplicateCount;
    }

    public static void Run()
    {
        var test1 = new List<int> { 1, 2, 3, 4, 5 };
        Console.WriteLine($"Test 1 (No duplicates): {CountDuplicates(test1)} (Expected: 0)");

        var test2 = new List<int> { 1, 2, 3, 1, 2, 4 };
        Console.WriteLine($"Test 2 (Some duplicates): {CountDuplicates(test2)} (Expected: 2)");

        var test3 = new List<int> { 1, 1, 1, 1, 1 };
        Console.WriteLine($"Test 3 (All duplicates): {CountDuplicates(test3)} (Expected: 4)");
    }
}