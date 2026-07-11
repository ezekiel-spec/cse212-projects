using System;
using System.Collections.Generic;

/// <summary>
/// A queue where items are removed based on their priority. The item with the 
/// highest priority number is removed first. If priorities are equal, the item 
/// closer to the front (FIFO) is removed first.
/// </summary>
public class PriorityQueue
{
    // Self-contained inner class to manage values and priorities cleanly
    public class PriorityItem
    {
        public string Value { get; set; }
        public int Priority { get; set; }

        public PriorityItem(string value, int priority)
        {
            Value = value;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"{Value} (Pri:{Priority})";
        }
    }

    private readonly List<PriorityItem> _queue = new();

    public int Length => _queue.Count;

    /// <summary>
    /// Add an item to the back of the priority queue
    /// </summary>
    public void Enqueue(string value, int priority)
    {
        var item = new PriorityItem(value, priority);
        _queue.Add(item);
    }

    /// <summary>
    /// Remove and return the item with the highest priority.
    /// If there is a tie, the item closest to the front of the queue is taken.
    /// </summary>
    public string Dequeue()
    {
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        // Find the index of the item with the highest priority
        int highPriorityIndex = 0;
        for (int i = 1; i < _queue.Count; i++)
        {
            // Using strict '>' ensures we keep the first one found (FIFO tie-breaker)
            if (_queue[i].Priority > _queue[highPriorityIndex].Priority)
            {
                highPriorityIndex = i;
            }
        }

        // Save the value, remove the item from the list, and return it
        string value = _queue[highPriorityIndex].Value;
        _queue.RemoveAt(highPriorityIndex);
        return value;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}