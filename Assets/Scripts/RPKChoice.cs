public class RPKChoice
{
    private RPKChoice(string p_toString) { _toString = p_toString; }
    public static readonly RPKChoice Rock = new RPKChoice("Rock");
    public static readonly RPKChoice Paper = new RPKChoice("Paper");
    public static readonly RPKChoice Scissors = new RPKChoice("Scissors");
    private string _toString;

    public static bool operator >(RPKChoice a, RPKChoice b)
    {
        if (a == RPKChoice.Rock && b == RPKChoice.Scissors) return true;
        if (a == RPKChoice.Paper && b == RPKChoice.Rock) return true;
        if (a == RPKChoice.Scissors && b == RPKChoice.Paper) return true;
        return false;
    }

    public static bool operator <(RPKChoice a, RPKChoice b)
    {
        return !(a > b) && !(a == b);
    }

    public override string ToString()
    {
        return _toString;
    }
}
