public class RPKThrow
{
    public RPKChoice throwType { get; private set; }
    public Player me;
    public Player opponent;
    public string name { get; private set; }
    private int disabledTimer = 0;

    public RPKThrow(string p_name, RPKChoice p_throwType, Player p_me, Player p_opponent)
    {
        name = p_name;
        throwType = p_throwType;
        me = p_me;
        opponent = p_opponent;
        OnParryLost = () => { me.character.Damage(1); };
        OnParry = () => { opponent.character.Disable(opponent.throwSelection); };
        RPKManager.ThrowFinished += PostThrowUpdate;
    }

    public static RPKThrow DefaultRPKThrow(RPKChoice p_throwType, Player p_me, Player p_opponent)
    {
        string name = "Default " + p_throwType.ToString();
        RPKThrow rpkThrow = new RPKThrow(name, p_throwType, p_me, p_opponent);
        rpkThrow.OnWin += () => { p_opponent.character.Damage(1); };
        return rpkThrow;
    }

    public delegate void ThrowOutcomeHandler();
    public ThrowOutcomeHandler OnWin = () => { };
    public ThrowOutcomeHandler OnLose = () => { };
    public ThrowOutcomeHandler OnClash = () => { };
    public ThrowOutcomeHandler OnParryLost = () => { };
    public ThrowOutcomeHandler OnParry = () => { };

    public bool isDisabled() { return disabledTimer > 0 || me.character.isDisabled(throwType); }

    private void PostThrowUpdate() { disabledTimer--; }

    public static void ResolveThrow(Player a, Player b)
    {
        if (a.throwSelection == b.throwSelection)
        {
            if (a.isParrying == b.isParrying)
            {
                a.GetSelectedThrow().OnClash();
                b.GetSelectedThrow().OnClash();
            }
            else if (a.isParrying && !b.isParrying)
            {
                a.GetSelectedThrow().OnParry();
                a.GetSelectedThrow().OnClash();
            }
            else if (!a.isParrying && b.isParrying)
            {
                b.GetSelectedThrow().OnParry();
                b.GetSelectedThrow().OnClash();
            }
        }
        else if (a.throwSelection > b.throwSelection)
        {
            if (!a.isParrying)
            {
                a.GetSelectedThrow().OnWin();
                if (b.isParrying)
                    b.GetSelectedThrow().OnParryLost();
                else
                    b.GetSelectedThrow().OnLose();
            }
        }
        else
        {
            if (!b.isParrying)
            {
                b.GetSelectedThrow().OnWin();
                if (a.isParrying)
                    a.GetSelectedThrow().OnParryLost();
                else
                    a.GetSelectedThrow().OnLose();
            }
        }
    }
}
