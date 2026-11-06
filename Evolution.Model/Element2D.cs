namespace Evolution.Model;

public class Element2D(byte initialValue) : Element(initialValue)
{
    public Element2D? Top
    {
        get; protected set
        {
            if (CheckIfDisposed() && value is not null)
                return;

            field = value;
        }
    }
    public Element2D? Bottom
    {
        get; protected set
        {
            if (CheckIfDisposed() && value is not null)
                return;

            field = value;
        }
    }

    public override void Dispose()
    {
        Top?.Bottom = null;
        Bottom?.Top = null;
        base.Dispose();
    }
}