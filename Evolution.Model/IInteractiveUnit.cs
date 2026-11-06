namespace Evolution.Model;

public interface IInteractiveUnit<TUnit> : IDisposable
{
    bool IsDisposed { get; }
    bool Interact(TUnit other);
}