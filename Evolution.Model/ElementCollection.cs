using System.Collections.ObjectModel;

namespace Evolution.Model;

public class ElementCollection<TElement> : Collection<TElement>
    where TElement : Element
{
    private int _current = 0;
    private readonly Random _randomizer = new();

    public ElementCollection(IEnumerable<TElement> elements)
        : base([..elements])
    {
        OddElementToIgnore = Count % 2 == 1 ? Randomizer.Next(Count) : null;
    }

    protected int Current => _current;
    private int? OddElementToIgnore { get; set; }
    protected Random Randomizer => _randomizer;

    public TElement? GetNextElement()
    {
        if (Current >= Count)
        {
            _current = 0;
            return null;
        }

        TElement element = this[Current];
        _current++;
        return element;
    }

    public (TElement left, TElement right)? GetNextPair()
    {
        if (Current == OddElementToIgnore)
            _current++;

        TElement? left = GetNextElement();

        if (Current == OddElementToIgnore)
            _current++;

        TElement? right = GetNextElement();

        if (left is not null && right is not null)
            return (left, right);

        _current = 0;
        return null;
    }

    public virtual void Shuffle()
    {
        Console.WriteLine("Elements are shuffled.");
        _current = 0;

        for (int i = 0; i < Count; i++)
        {
            if (this[i].IsDisposed)
            {
                RemoveAt(i);
                i--;
                continue;
            }
        }

        for (int i = 0; i < Count - 1; i++)
        {
            int j = Randomizer.Next(i + 1, Count);
            (this[i], this[j]) = (this[j], this[i]);
        }

        OddElementToIgnore = Count % 2 == 1 ? Randomizer.Next(Count) : null;
    }
}