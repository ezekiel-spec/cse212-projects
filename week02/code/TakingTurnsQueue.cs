using System;

/// <summary>
/// This queue is circular. People stay in the queue based on their remaining turns.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }
        else
        {
            Person person = _people.Dequeue();

            // Check if the person has infinite turns (0 or less)
            if (person.Turns <= 0)
            {
                _people.Enqueue(person);
            }
            // If they have multiple turns left, decrement and put them back
            else if (person.Turns > 1)
            {
                person.Turns -= 1;
                _people.Enqueue(person);
            }

            return person;
        }
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}