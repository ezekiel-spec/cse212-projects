using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with distinct priorities: Low (1), High (5), Medium (3).
    // Expected Result: High, Medium, Low
    // Defect Found: The original code failed to properly loop through and remove the highest priority item correctly. Fixed in PriorityQueue.cs.
    public void TestPriorityQueue_1()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("Low", 1);
        queue.Enqueue("High", 5);
        queue.Enqueue("Medium", 3);

        Assert.AreEqual("High", queue.Dequeue());
        Assert.AreEqual("Medium", queue.Dequeue());
        Assert.AreEqual("Low", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items where some have duplicate highest priorities: ItemA (2), ItemB (5), ItemC (5).
    // Expected Result: ItemB, ItemC, ItemA (FIFO tie-breaker)
    // Defect Found: The loop inside Dequeue used `>=` instead of `>`, which broke FIFO rules by pulling the last added duplicate priority instead of the first. Fixed to strict `>`.
    public void TestPriorityQueue_2()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("ItemA", 2);
        queue.Enqueue("ItemB", 5);
        queue.Enqueue("ItemC", 5);

        Assert.AreEqual("ItemB", queue.Dequeue());
        Assert.AreEqual("ItemC", queue.Dequeue());
        Assert.AreEqual("ItemA", queue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty priority queue.
    // Expected Result: InvalidOperationException thrown with message "The queue is empty."
    // Defect Found: None. Validated that queue cleanly checks for zero items before executing removal logic.
    public void TestPriorityQueue_Empty()
    {
        var queue = new PriorityQueue();

        try
        {
            queue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}