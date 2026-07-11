using System;
using System.Collections.Generic;

/// <summary>
/// A basic implementation of a Queue (First-In-First-Out)
/// </summary>
public class PersonQueue
{
    private readonly List<Person> _queue = new();

    public int Length => _queue.Count;

    /// <summary>
    /// Add a person to the back of the queue
    /// </summary>
    public void Enqueue(Person person)
    {
        _queue.Add(person);
    }

    /// <summary>
    /// Remove and return the person from the front of the queue
    /// </summary>
    public Person Dequeue()
    {
        if (Length == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        var person = _queue[0];
        _queue.RemoveAt(0);
        return person;
    }

    public bool IsEmpty()
    {
        return Length == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}