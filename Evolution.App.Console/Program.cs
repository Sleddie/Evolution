using Evolution.Model;
using Evolution.Process;

Simulation<Element> simulation = new();
bool repeat = true;

do
{
    DisplayElements(simulation.Params);

    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.Tab:
            simulation.SimulateNextStep();
            break;
        case ConsoleKey.Enter:
            simulation.SimulateCycle();
            break;
        //case ConsoleKey.Spacebar:
        //    simulation.SimulateAsync();
        //    break;
        case ConsoleKey.Escape:
            Console.WriteLine();
            repeat = false;
            break;
    }

    Console.WriteLine();
} while (repeat);

Console.ReadKey();

void DisplayElements<TElement>(Parameters<TElement> parameters)
    where TElement : Element
{
    foreach (TElement element in parameters.Elements)
    {
        Console.Write($"{element} ; ");
    }
}