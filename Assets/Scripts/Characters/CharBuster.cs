using UnityEngine;

public class CharBuster : Character
{
    public CharBuster(Player p_player, Player p_opponent, RectTransform myUI) :
        base(p_player, p_opponent, myUI)
    {
        name = "Buster";
    }

    protected override void ConfigureThrows(Player p_player, Player p_opponent)
    {
        throws.Add(new Throw_DefaultRock(p_player, p_opponent));
        throws.Add(new Throw_DefaultPaper(p_player, p_opponent));
        throws.Add(new Throw_DefaultScissors(p_player, p_opponent));
    }
}
