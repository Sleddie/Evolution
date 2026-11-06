using Evolution.Model;

namespace Evolution.Process;

public class Simulation<TElement>
    where TElement : Element
{
    private const int CyclesInterval = 5000;

    public Parameters<TElement> Params { get; } = Parameters<TElement>.GetRandomParameters(16);

    public void SimulateNextStep()
    {
        Params.CollideNext();
    }

    public void SimulateCycle()
    {
        Params.CollideAll();
    }

    public async Task SimulateAsync()
    {
        await Task.Delay(CyclesInterval);
        await Task.Run(Params.CollideAll);
    }
}