using System;
using System.Collections.Generic;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' where each element is a multiple of
    /// 'number' starting at 'number' itself. For example, MultiplesOf(3, 5) will return
    /// [3, 6, 9, 12, 15].
    /// </summary>
    /// <returns>array of doubles</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // STEP 1: Create an array of doubles to hold our results.
        // The size of this array should match the 'length' parameter passed in.
        double[] result = new double[length];

        // STEP 2: Loop through each index of the array from 0 up to length - 1.
        // We can calculate each multiple by multiplying our starting number by (i + 1).
        // For index 0, it's number * 1. For index 1, it's number * 2, and so on.
        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        // STEP 3: Return the populated array so it can be verified by the tests.
        return result;
    }

    /// <summary>
    /// Rotate the 'data' list to the right by the 'amount'. For example, if the data is 
    /// List {1, 2, 3, 4, 5, 6, 7, 8, 9} and amount is 3, then the list after the function runs should be 
    /// List {7, 8, 9, 1, 2, 3, 4, 5, 6}. The value of amount will be in the range of 1 and
    /// data.Count, inclusive.
    ///
    /// Because we are modifying the list itself, this function doesn't return anything.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // STEP 1: Check for edge cases where rotation isn't needed.
        // If the list is empty, has 1 item, or the rotation amount matches the count exactly,
        // we can just exit early since the list wouldn't change.
        if (data == null || data.Count <= 1 || amount == data.Count)
        {
            return;
        }

        // STEP 2: Make sure the amount doesn't exceed the list size using the modulo operator.
        // Since the prompt states amount is between 1 and data.Count inclusive, this keeps it safe.
        int rotationAmount = amount % data.Count;
        if (rotationAmount == 0)
        {
            return;
        }

        // STEP 3: Split the list into two distinct parts using GetRange.
        // The split point needs to be calculated from the end of the list moving backwards by rotationAmount.
        int splitIndex = data.Count - rotationAmount;

        // Get the back slice that needs to move to the front
        List<int> backSlice = data.GetRange(splitIndex, rotationAmount);
        
        // Get the front slice that will move to the back
        List<int> frontSlice = data.GetRange(0, splitIndex);

        // STEP 4: Modify the original data list in-place to reflect the new rotation.
        // We wipe the list clean first, then add the back slice first followed by the front slice.
        data.Clear();
        data.AddRange(backSlice);
        data.AddRange(frontSlice);
    }
}