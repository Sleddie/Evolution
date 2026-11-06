using Evolution.Model;

namespace Evolution.Process;

public partial class Parameters<TElement>(IEnumerable<TElement> elements)
    where TElement : Element
{
    public Parameters(params TElement[] elements)
        : this((IEnumerable<TElement>)elements) { }

    protected ElementCollection<TElement> _elements = new(elements);

    public IReadOnlyCollection<TElement> Elements => _elements.AsReadOnly();

    public bool CollideNext()
    {
        var pairNullable = _elements.GetNextPair();

        if (pairNullable is null)
            return false;

        var (left, right) = pairNullable.Value;
        bool isCorrect = left.Interact(right);

        return isCorrect;
    }

    public void CollideAll()
    {
        while (CollideNext()) { }
    }
}