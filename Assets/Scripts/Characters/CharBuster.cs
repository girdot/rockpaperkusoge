using UnityEngine;

public class CharBuster : Character
{
    public override string name { get { return "Buster"; } }

    // Define throws
    private Throw_DefaultRock _defaultRock;
    private Throw_DefaultPaper _defaultPaper;
    private Throw_DefaultScissors _defaultScissors;

    public CharBuster(Player p_player, Player p_opponent, RectTransform myUI)
        : base(p_player, p_opponent, myUI)
    {
        // Initialize throws
        _defaultRock = new Throw_DefaultRock(player, opponent);
        _defaultPaper = new Throw_DefaultPaper(player, opponent);
        _defaultScissors = new Throw_DefaultScissors(player, opponent);
    }

    // Retrieve throws
    protected override Throw RockThrowOption() { return _defaultRock; }
    protected override Throw PaperThrowOption() { return _defaultPaper; }
    protected override Throw ScissorsThrowOption() { return _defaultScissors; }
}
