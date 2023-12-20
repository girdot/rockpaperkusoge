using System.Collections.Generic;

public class Throw
{
    public enum ThrowType
    {
        Rock,
        Paper,
        Scissors
    }

    private ThrowType throwType;
    public string name;
    public List<ThrowEffect> onWinEffects = new List<ThrowEffect>();
    public List<ThrowEffect> onClashEffects = new List<ThrowEffect>();
    public List<ThrowEffect> onLoseEffects = new List<ThrowEffect>();
    public List<ThrowEffect> onThrowEffects = new List<ThrowEffect>();
    public List<ThrowEffect> onParryEffects = new List<ThrowEffect>();
    public List<ThrowEffect> onParryClashEffects = new List<ThrowEffect>();
    public List<ThrowEffect> onParryLoseEffects = new List<ThrowEffect>();
    public List<ThrowEffect> onParryWinEffects = new List<ThrowEffect>();
    public bool is_disabled;

    public static bool operator !=(Throw a, Throw b)
    {
        if ((b is null) != (a is null)) return true;
        if (a is null && b is null) return false;
        return a.throwType == b.throwType;
    }

    public static bool operator ==(Throw a, Throw b)
    {
        if ((b is null) != (a is null)) return false;
        if (a is null && b is null) return true;
        return a.throwType == b.throwType;
    }

    public static bool operator >(Throw a, Throw b)
    {
        switch( a.throwType )
        {
            case Throw.ThrowType.Rock:
                if(b.throwType == Throw.ThrowType.Scissors)
                    return true;
                break;
            case Throw.ThrowType.Paper:
                if(b.throwType == Throw.ThrowType.Rock)
                    return true;
                break;
            case Throw.ThrowType.Scissors:
                if(b.throwType == Throw.ThrowType.Paper)
                    return true;
                break;
        }
        return false;
    }
    public static bool operator <(Throw a, Throw b)
    {
        switch( a.throwType )
        {
            case Throw.ThrowType.Rock:
                if(b.throwType == Throw.ThrowType.Scissors)
                    return false;
                break;
            case Throw.ThrowType.Paper:
                if(b.throwType == Throw.ThrowType.Rock)
                    return false;
                break;
            case Throw.ThrowType.Scissors:
                if(b.throwType == Throw.ThrowType.Paper)
                    return false;
                break;
        }
        return true;
    }
    public Throw(string p_name, ThrowType p_throwType, Player p_player, Player p_opponent)
    {
        name = p_name;
        throwType = p_throwType;
        onWinEffects.Add( new ThrowEffectDamageOpponent( p_opponent, 1 ) );
    }
    private bool is_available()
    {
        return is_disabled ? false : true;
    }
}

public abstract class ThrowEffect
{
    public abstract void execute();
}

public class ThrowEffectDamageOpponent : ThrowEffect
{
    private int damage;
    private Player opponent;

    public ThrowEffectDamageOpponent( Player p_opponent, int p_damage )
    {
        damage = p_damage;
        opponent = p_opponent;
    }

    public override void execute(){
        opponent.character.Damage( damage );
    }
}
