using System.Collections.Generic;
using System;

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
    public event EventHandler WonHandler;
    public event EventHandler LostHandler;
    public event EventHandler ClashedHandler;
    public List<ThrowEffect> throwEffects = new List<ThrowEffect>();

    public Throw(string p_name, ThrowType p_throwType, Player p_player, Player p_opponent)
    {
        name = p_name;
        throwType = p_throwType;
        throwEffects.Add( new ThrowEffectDealDmgOnWin(this,  p_opponent, 1 ) );
    }

    public static void ResolveThrow( Throw a, Throw b )
    {
        if( a.Equals( b ) )
        {
            a.OnClash( EventArgs.Empty );
            b.OnClash( EventArgs.Empty );
        } else if( a > b )
        {
            a.OnWin( EventArgs.Empty );
            b.OnLose( EventArgs.Empty );
        } else
        {
            a.OnLose( EventArgs.Empty );
            b.OnWin( EventArgs.Empty );
        }
    }

    public override bool Equals( object obj ){
        if( obj is Throw opponent_throw )
            return throwType == opponent_throw.throwType;
        return false;
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

    protected virtual void OnWin(EventArgs e)
    {
        WonHandler?.Invoke( this, e );
    }

    protected virtual void OnLose(EventArgs e)
    {
        LostHandler?.Invoke( this, e );
    }

    protected virtual void OnClash(EventArgs e)
    {
        ClashedHandler?.Invoke( this, e );
    }
}

public abstract class ThrowEffect
{
    public virtual void ExecuteOnWin(Object sender, EventArgs e){}
    public virtual void ExecuteOnLose(Object sender, EventArgs e){}
    public virtual void ExecuteOnClash(Object sender, EventArgs e){}

    public  ThrowEffect( Throw p_throw )
    {
        p_throw.WonHandler += ExecuteOnWin;
        p_throw.LostHandler += ExecuteOnLose;
        p_throw.ClashedHandler += ExecuteOnLose;
    }
}

public class ThrowEffectDealDmgOnWin : ThrowEffect
{
    private int damage;
    private Player opponent;

    public ThrowEffectDealDmgOnWin( Throw p_throw, Player p_opponent, int p_damage ) : base(p_throw)
    {
        damage = p_damage;
        opponent = p_opponent;
    }

    public override void ExecuteOnWin(Object sender, EventArgs e){
        opponent.character.Damage( damage );
    }
}
