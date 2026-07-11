using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    // Scenario: Add a person with 2 turns and ensure they get their first turn,
    // then check if they stay in the queue correctly.
    // Expected Result: Bob, Tim, Bob
    // Defect Found: The underlying PersonQueue was adding and removing from index 0, 
    // making it behave like a Stack (LIFO) instead of a Queue (FIFO). Fixed PersonQueue to use Add() for back-enqueue.
    public void TestTakingTurnsQueue_FiniteRepetition()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Bob", 2);
        queue.AddPerson("Tim", 1);

        Assert.AreEqual("Bob", queue.GetNextPerson().Name);
        Assert.AreEqual("Tim", queue.GetNextPerson().Name);
        Assert.AreEqual("Bob", queue.GetNextPerson().Name);
    }

    [TestMethod]
    // Scenario: Add a person mid-way through a queue cycle and verify FIFO order.
    // Expected Result: Bob, Tim, Sue
    // Defect Found: LIFO bug in PersonQueue caused newly added mid-way players to pop out-of-order. Fixed by correcting Enqueue/Dequeue mechanics.
    public void TestTakingTurnsQueue_AddPlayerMidway()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Bob", 1);
        queue.AddPerson("Tim", 1);

        Assert.AreEqual("Bob", queue.GetNextPerson().Name);
        
        queue.AddPerson("Sue", 1);
        Assert.AreEqual("Tim", queue.GetNextPerson().Name);
        Assert.AreEqual("Sue", queue.GetNextPerson().Name);
    }

    [TestMethod]
    // Scenario: Add a person with 0 turns to ensure they have infinite turns.
    // Expected Result: Bob, Tim, Bob, Bob
    // Defect Found: PersonQueue LIFO behavior returned the wrong order initially. Fixed to ensure true FIFO rotation.
    public void TestTakingTurnsQueue_ForeverZero()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Bob", 0);
        queue.AddPerson("Tim", 1);

        Assert.AreEqual("Bob", queue.GetNextPerson().Name);
        Assert.AreEqual("Tim", queue.GetNextPerson().Name);
        Assert.AreEqual("Bob", queue.GetNextPerson().Name);
        Assert.AreEqual("Bob", queue.GetNextPerson().Name);
    }

    [TestMethod]
    // Scenario: Add a person with a negative turn value to ensure they have infinite turns.
    // Expected Result: Tim, Sue, Tim
    // Defect Found: PersonQueue LIFO ordering bug caused incorrect extraction. Rectified by fixing underlying array tracking.
    public void TestTakingTurnsQueue_ForeverNegative()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Tim", -5);
        queue.AddPerson("Sue", 1);

        Assert.AreEqual("Tim", queue.GetNextPerson().Name);
        Assert.AreEqual("Sue", queue.GetNextPerson().Name);
        Assert.AreEqual("Tim", queue.GetNextPerson().Name);
    }

    [TestMethod]
    // Scenario: Attempt to get a person from an empty queue.
    // Expected Result: InvalidOperationException thrown.
    // Defect Found: None. Handled cleanly.
    public void TestTakingTurnsQueue_Empty()
    {
        var queue = new TakingTurnsQueue();

        try
        {
            queue.GetNextPerson();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("No one in the queue.", e.Message);
        }
    }
}