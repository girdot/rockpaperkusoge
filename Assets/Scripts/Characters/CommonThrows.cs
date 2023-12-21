// This script contains throws and throweffects that are used across one or more
// characters.

// THROWS
class Throw_DefaultRock : Throw 
{
    public Throw_DefaultRock(Player player, Player opponent)
        : base( player, opponent ) {}
    protected override string SetName(){ return "Default Rock"; }
    protected override ThrowType SetThrowType(){ return ThrowType.Rock; }
    protected override void addThrowEffects(Player p_player, Player p_opponent)
    {
        throwEffects.Add( new TEffect_DmgOnWin( p_player, p_opponent, 1) );
    }
}

class Throw_DefaultPaper : Throw {
    public Throw_DefaultPaper(Player player, Player opponent)
        : base( player, opponent ) {}
    protected override string SetName(){ return "Default Paper"; }
    protected override ThrowType SetThrowType(){ return ThrowType.Paper; }
    protected override void addThrowEffects(Player p_player, Player p_opponent)
    {
        throwEffects.Add( new TEffect_DmgOnWin( p_player, p_opponent, 1) );
    }
}

class Throw_DefaultScissors : Throw {
    public Throw_DefaultScissors(Player player, Player opponent)
        : base( player, opponent ) {}
    protected override string SetName(){ return "Default Scissors"; }
    protected override ThrowType SetThrowType(){ return ThrowType.Scissors; }
    protected override void addThrowEffects(Player p_player, Player p_opponent)
    {
        throwEffects.Add( new TEffect_DmgOnWin( p_player, p_opponent, 1) );
    }
}

// THROW EFFECTS

public class TEffect_DmgOnWin : ThrowEffect
{
    private int damage;

    public TEffect_DmgOnWin(
            Player p_player,
            Player p_opponent,
            int p_damage) : base( p_player, p_opponent )
    {
        damage = p_damage;
    }

    protected override void OnWin() { opponent.character.Damage(damage); }
}
