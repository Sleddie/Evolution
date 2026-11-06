using Evolution.Model;

namespace Evolution.Process;

public partial class Parameters<TElement>
{
    protected const int MaxInitialAmount = 128;

    protected static Random GeneralRandom { get; } = new();

    public static Parameters<TElement> GetRandomParameters(int amount)
    {
        if (amount < 0)
            ArgumentOutOfRangeException.ThrowIfNegative(amount, nameof(amount));

        if (amount == 0)
            return new();

        List<TElement> elements = [];

        for (int i = 0; i < amount; i++)
        {
            elements.Add((TElement)GeneralRandom.Next());
        }

        return new(elements);
    }

    public static Parameters<TElement> GetRandomParameters() =>
        GetRandomParameters(GeneralRandom.Next(MaxInitialAmount));
}