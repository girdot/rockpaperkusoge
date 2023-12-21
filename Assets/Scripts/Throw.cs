using System.Collections.Generic;

public enum ThrowType { Rock, Paper, Scissors }
public enum ThrowOutcome { Won, Lost, Clashed }

public abstract class Throw
{
    public abstract string name { get; }
    public abstract ThrowType throwType { get; }
    private List<ThrowEffect> throwEffects = new List<ThrowEffect>();
    protected abstract void addThrowEffects(Player p_player, Player p_opponent);
    private delegate void ThrowOutcomeHandler(ThrowOutcome throwOutcome);
    private ThrowOutcomeHandler throwOutcomeCallback;

    public Throw(Player p_player, Player p_opponent)
    {
        addThrowEffects(p_player, p_opponent);
    }

    public void AddThrowEffect(ThrowEffect throwEffect)
    {
        throwEffects.Add(throwEffect);
        throwOutcomeCallback += throwEffect.Execute;
    }

    public static void ResolveThrow(Throw a, Throw b)
    {
        if (a.Equals(b))
        {
            a.throwOutcomeCallback(ThrowOutcome.Clashed);
            b.throwOutcomeCallback(ThrowOutcome.Clashed);
        }
        else if (a > b)
        {
            a.throwOutcomeCallback(ThrowOutcome.Won);
            b.throwOutcomeCallback(ThrowOutcome.Lost);
        }
        else
        {
            a.throwOutcomeCallback(ThrowOutcome.Lost);
            b.throwOutcomeCallback(ThrowOutcome.Won);
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

