namespace Evolution.Model;

public class Element3D(byte initialValue) : Element2D(initialValue)
{
    public Element3D? Front
    {
        get; protected set
        {
            if (IsDisposed)
                return;

            field = value;
        }
    }
    public Element3D? Back
    {
        get; protected set
        {
            if (IsDisposed)
                return;

            field = value;
        }
    }

    public override void Dispose()
    {
        Front?.Back = null;
        Back?.Front = null;
        base.Dispose();
    }
}