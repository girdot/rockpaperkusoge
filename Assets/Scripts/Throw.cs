using System.Collections.Generic;
using System;

public class Throw
{
    public enum ThrowType { Rock, Paper, Scissors }
    public enum ThrowOutcome { Won, Lost, Clashed }

    public string name { get; private set; }
    public event EventHandler<ThrowOutcome> handler;
    public List<ThrowEffect> throwEffects = new List<ThrowEffect>();
    private ThrowType throwType;

    public Throw(ThrowType p_throwType, Player p_player, Player p_opponent)
    {
        name = "Default " + p_throwType.ToString();
        throwType = p_throwType;
        addThrowEffects(p_player, p_opponent);
    }

    public virtual void addThrowEffects(Player p_player, Player p_opponent)
    {
        throwEffects.Add(new TEffectDmgOnWin(ref handler, p_opponent, 1));
    }

    public static void ResolveThrow(Throw a, Throw b)
    {
        if (a.Equals(b))
        {
            a.handler?.Invoke(a, ThrowOutcome.Clashed);
            b.handler?.Invoke(b, ThrowOutcome.Clashed);
        }
        else if (a > b)
        {
            a.handler?.Invoke(a, ThrowOutcome.Won);
            b.handler?.Invoke(b, ThrowOutcome.Lost);
        }
        else
        {
            a.handler?.Invoke(a, ThrowOutcome.Lost);
            b.handler?.Invoke(b, ThrowOutcome.Won);
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
    public virtual void ExecuteOnWin() { }
    public virtual void ExecuteOnLose() { }
    public virtual void ExecuteOnClash() { }

    public ThrowEffect(ref EventHandler<Throw.ThrowOutcome> h) { h += Execute; }

    private void Execute(Object sender, Throw.ThrowOutcome outcome)
    {
        if (outcome == Throw.ThrowOutcome.Won) ExecuteOnWin();
        else if (outcome == Throw.ThrowOutcome.Lost) ExecuteOnLose();
        else ExecuteOnClash();
    }
}

public class TEffectDmgOnWin : ThrowEffect
{
    private int damage;
    private Player opponent;

    public TEffectDmgOnWin(
            ref EventHandler<Throw.ThrowOutcome> h,
            Player p_opponent,
            int p_damage) : base(ref h)
    {
        damage = p_damage;
        opponent = p_opponent;
    }

    public override void ExecuteOnWin() { opponent.character.Damage(damage); }
}
