namespace Evolution.Model;

public class Element : IInteractiveUnit<Element>
{
    protected const byte Divider = 10;
    protected const byte Decrement = 3;

    public Element(byte initialValue)
    {
        Value = initialValue;
    }

    public bool IsDisposed { get; private set; }

    public byte Value
    {
        get;
        protected set
        {
            if (IsDisposed)
                return;

            field = (byte)(value % Divider);
        }
    }
    public bool IsOdd => Value % 2 == 1;

    public Element? Left
    {
        get;
        protected set
        {
            if (IsDisposed)
                return;

            field = value;
        }
    }
    public Element? Right
    {
        get;
        protected set
        {
            if (IsDisposed)
                return;

            field = value;
        }
    }

    public override string ToString()
    {
        string displayString = $"{Value}";

        if (Left is not null)
            displayString = '+' + displayString;

        if (Right is not null)
            displayString += displayString + '+';

        return displayString;
    }

    public virtual bool Interact(Element right)
    {
        if (IsDisposed)
            return false;

        byte rightValue = right.Value;
        var sum = Value + rightValue;

        if (sum == Divider)
            return Connect(right);

        if (!IsOdd && !right.IsOdd)
            return Ignore(right);

        if (IsOdd && right.IsOdd)
            return Balance(right);

        return Compete(right);
    }

    protected byte SetValue(int newValue)
    {
        if (IsDisposed)
            return 0;

        return Value = (byte)(newValue % Divider);
    }

    protected virtual bool Connect(Element right)
    {
        if (IsDisposed)
            return false;

        Right?.Left = null;
        Right = right;
        right.Left?.Right = null;
        right.Left = this;
        return true;
    }

    protected virtual bool Ignore(Element right)
    {
        if (IsDisposed)
            return false;

        return true;
    }

    protected virtual bool Balance(Element right)
    {
        if (IsDisposed)
            return false;

        if (Value > right.Value)
        {
            SetValue(Value - Decrement);
            right.SetValue(right.Value + Decrement);
        }
        else if (Value < right.Value)
        {
            SetValue(Value + Decrement);
            right.SetValue(right.Value - Decrement);
        }

        return true;
    }

    protected virtual bool Compete(Element right)
    {
        if (IsDisposed)
            return false;

        int newValue = Value + right.Value;

        if (Value > right.Value)
        {
            SetValue(newValue);
            right.Dispose();
        }
        if (Value < right.Value)
        {
            right.SetValue(newValue);
            Dispose();
        }

        return true;
    }

    public virtual void Dispose()
    {
        Left?.Right = null;
        Right?.Left = null;
        IsDisposed = true;
    }

    public static implicit operator Element(byte value) => new(value);
    public static explicit operator byte(Element element) => element.Value;
    public static explicit operator Element(int value) => new((byte)(value % Divider));
    public static explicit operator int(Element element) => element.Value;
}