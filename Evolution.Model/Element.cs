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
            if (CheckIfDisposed())
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
            if (CheckIfDisposed() && value is not null)
                return;

            field = value;
        }
    }
    public Element? Right
    {
        get;
        protected set
        {
            if (CheckIfDisposed() && value is not null)
                return;

            field = value;
        }
    }

    public string DisplayName => IsDisposed ? "" : ToString();

    public override string ToString()
    {
        string displayString = $"{Value}";

        if (Left is not null)
            displayString = $"+{displayString}";

        if (Right is not null)
            displayString += '+';

        return displayString;
    }

    public byte SetValue(int newValue)
    {
        if (CheckIfDisposed())
            return 0;

        return Value = (byte)(newValue % Divider);
    }

    public bool ReplaceLeft(Element newLeft)
    {
        if (CheckIfDisposed() || newLeft.CheckIfDisposed())
            return false;

        Left?.Right = null;
        Left = newLeft;
        newLeft.Right?.Left = null;
        newLeft.Right = this;
        return true;
    }

    public bool ReplaceRight(Element newRight)
    {
        if (CheckIfDisposed() || newRight.CheckIfDisposed())
            return false;

        Right?.Left = null;
        Right = newRight;
        newRight.Left?.Right = null;
        newRight.Left = this;
        return true;
    }

    public virtual bool Interact(Element right)
    {
        if (CheckIfDisposed() || right.CheckIfDisposed())
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

    protected virtual bool Connect(Element right)
    {
        if (IsDisposed)
            return false;

        if (Right == right && right.Left == this)
        {
            Console.WriteLine($"Elements {this} and {right} are already connected.");
            return true;
        }

        Console.WriteLine($"Elements {this} and {right} connect.");
        ReplaceRight(right);
        return true;
    }

    protected virtual bool Ignore(Element right)
    {
        if (IsDisposed)
            return false;

        Console.WriteLine($"Elements {this} and {right} don't interact.");
        return true;
    }

    protected virtual bool Balance(Element right)
    {
        if (IsDisposed)
            return false;

        Console.Write($"Elements {this} and {right} balance. ");

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

        Console.WriteLine($"Now are {this} and {right}.");
        return true;
    }

    protected virtual bool Compete(Element right)
    {
        if (IsDisposed)
            return false;

        Console.Write($"Elements {this} and {right} compete. ");
        int newValue = Value + right.Value;
        bool leftWins = Value > right.Value;
        Element winner = leftWins ? this : right;
        Element loser = leftWins ? right : this;

        if (winner.Left is null && loser.Left is not null)
        {
            winner.ReplaceLeft(loser.Left);
        }

        if (winner.Right is null && loser.Right is not null)
        {
            winner.ReplaceRight(loser.Right);
        }

        winner.SetValue(newValue);
        loser.Dispose();

        Console.Write($"Element {winner} wins; ");
        loser.CheckIfDisposed();
        return true;
    }

    public virtual bool CheckIfDisposed()
    {
        if (IsDisposed)
        {
            Console.WriteLine($"Element {this} is disposed.");
        }

        return IsDisposed;
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