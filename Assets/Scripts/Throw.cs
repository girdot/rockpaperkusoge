using System.Collections.Generic;

public enum ThrowType { Rock, Paper, Scissors }
public enum ThrowOutcome { Won, Lost, Clashed,
    ParrySuccess,
    ParryLost
}

public abstract class Throw
{
    public abstract string name { get; }
    public abstract ThrowType throwType { get; }
    private List<ThrowEffect> throwEffects = new List<ThrowEffect>();
    protected abstract void addThrowEffects(Player p_player, Player p_opponent);
    private delegate void ThrowOutcomeHandler(ThrowOutcome throwOutcome);
    private ThrowOutcomeHandler throwOutcomeCallback;
    private Player player;
    private Player opponent;

    public Throw(Player p_player, Player p_opponent)
    {
        player = p_player;
        opponent = p_opponent;
        addThrowEffects(p_player, p_opponent);
    }

    public void AddThrowEffect(ThrowEffect throwEffect)
    {
        throwEffects.Add(throwEffect);
        throwOutcomeCallback += throwEffect.Execute;
    }

    public static void ResolveThrow(Throw a, bool a_parry, Throw b, bool b_parry)
    {
        if (a.Equals(b))
        {
            if(a_parry == b_parry)
            {
                a.throwOutcomeCallback(ThrowOutcome.Clashed);
                b.throwOutcomeCallback(ThrowOutcome.Clashed);
            }
            else if(a_parry && !b_parry)
            {
                a.throwOutcomeCallback(ThrowOutcome.ParrySuccess);
                a.throwOutcomeCallback(ThrowOutcome.Clashed);
            }
            else if(!a_parry && b_parry)
            {
                b.throwOutcomeCallback(ThrowOutcome.ParrySuccess);
                b.throwOutcomeCallback(ThrowOutcome.Clashed);
            }
        }
        else if (a > b)
        {
            if( !a_parry )
            {
                a.throwOutcomeCallback(ThrowOutcome.Won);
                if(b_parry)
                    b.throwOutcomeCallback(ThrowOutcome.ParryLost);
                else
                    b.throwOutcomeCallback(ThrowOutcome.Lost);
            }
        }
        else
        {
            if( !b_parry )
            {
                b.throwOutcomeCallback(ThrowOutcome.Won);
                if(a_parry)
                    a.throwOutcomeCallback(ThrowOutcome.ParryLost);
                else
                    a.throwOutcomeCallback(ThrowOutcome.Lost);
            }
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
    protected virtual void OnParrySuccess()
    {
        opponent.character.DisableThrowType( opponent.selectedThrow.throwType, 1 );
    }
    protected virtual void OnParryLost()
    {
        player.character.Damage( 1 );
    }
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
        else if (outcome == ThrowOutcome.ParrySuccess) OnParrySuccess();
        else if (outcome == ThrowOutcome.ParryLost) OnParryLost();
        else OnClash();
    }
}

