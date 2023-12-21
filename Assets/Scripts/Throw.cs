using System.Collections.Generic;

public enum ThrowType { Rock, Paper, Scissors }
public enum ThrowOutcome { Won, Lost, Clashed }

public abstract class Throw
{
    public string name { get; private set; }
    public List<ThrowEffect> throwEffects = new List<ThrowEffect>();
    protected ThrowType throwType;
    protected abstract string SetName();
    protected abstract ThrowType SetThrowType();
    protected abstract void addThrowEffects(Player p_player, Player p_opponent);

    public Throw(Player p_player, Player p_opponent)
    {
        name = SetName();
        throwType = SetThrowType();
        addThrowEffects(p_player, p_opponent);
    }

    public static void ResolveThrow(Throw a, Throw b)
    {
        if (a.Equals(b))
        {
            foreach (ThrowEffect throwEffect in a.throwEffects)
                throwEffect.Execute(ThrowOutcome.Clashed);
            foreach (ThrowEffect throwEffect in b.throwEffects)
                throwEffect.Execute(ThrowOutcome.Clashed);
        }
        else if (a > b)
        {
            foreach (ThrowEffect throwEffect in a.throwEffects)
                throwEffect.Execute(ThrowOutcome.Won);
            foreach (ThrowEffect throwEffect in b.throwEffects)
                throwEffect.Execute(ThrowOutcome.Lost);
        }
        else
        {
            foreach (ThrowEffect throwEffect in a.throwEffects)
                throwEffect.Execute(ThrowOutcome.Lost);
            foreach (ThrowEffect throwEffect in b.throwEffects)
                throwEffect.Execute(ThrowOutcome.Won);
        }
    }

    public static bool operator >(Throw a, Throw b)
    {
        if (a.throwType == ThrowType.Rock && b.throwType == ThrowType.Scissors)
            return true;
        if (a.throwType == ThrowType.Paper && b.throwType == ThrowType.Rock)
            return true;
        if (a.throwType == ThrowType.Scissors && b.throwType == ThrowType.Paper)
            return true;
        return false;
    }

    public static bool operator <(Throw a, Throw b)
    {
        return !(a > b) && !a.Equals(b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Throw opponent_throw)
            return throwType == opponent_throw.throwType;
        return false;
    }

    public override int GetHashCode() { return throwType.GetHashCode(); }
}

public abstract class ThrowEffect
{
    protected virtual void OnWin() { }
    protected virtual void OnLose() { }
    protected virtual void OnClash() { }
    protected Player player;
    protected Player opponent;

    public ThrowEffect(Player p_player, Player p_opponent)
    {
        player = p_player;
        opponent = p_opponent;
    }

    public void Execute(ThrowOutcome outcome)
    {
        if (outcome == ThrowOutcome.Won) OnWin();
        else if (outcome == ThrowOutcome.Lost) OnLose();
        else OnClash();
    }
}

