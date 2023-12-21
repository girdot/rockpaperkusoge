// This script contains throws and throweffects that are used across one or more
// characters.

// THROWS
class Throw_DefaultRock : Throw
{
    public override string name { get { return "Default Rock"; } }
    public override ThrowType throwType { get { return ThrowType.Rock; } }
    public Throw_DefaultRock(Player player, Player opponent)
        : base(player, opponent) { }
    protected override void addThrowEffects(Player p_player, Player p_opponent)
    {
        AddThrowEffect(new TEffect_DmgOnWin(p_player, p_opponent, 1));
    }
}

class Throw_DefaultPaper : Throw
{
    public override string name { get { return "Default Paper"; } }
    public override ThrowType throwType { get { return ThrowType.Paper; } }
    public Throw_DefaultPaper(Player player, Player opponent)
        : base(player, opponent) { }
    protected override void addThrowEffects(Player p_player, Player p_opponent)
    {
        AddThrowEffect(new TEffect_DmgOnWin(p_player, p_opponent, 1));
    }
}

class Throw_DefaultScissors : Throw
{
    public override string name { get { return "Default Scissors"; } }
    public override ThrowType throwType { get { return ThrowType.Scissors; } }
    public Throw_DefaultScissors(Player player, Player opponent)
        : base(player, opponent) { }
    protected override void addThrowEffects(Player p_player, Player p_opponent)
    {
        AddThrowEffect(new TEffect_DmgOnWin(p_player, p_opponent, 1));
    }
}

// THROW EFFECTS
public class TEffect_DmgOnWin : ThrowEffect
{
    private int damage;

    public TEffect_DmgOnWin(
            Player p_player,
            Player p_opponent,
            int p_damage) : base(p_player, p_opponent)
    {
        damage = p_damage;
    }

    protected override void OnWin() { opponent.character.Damage(damage); }
}
