using UnityEngine.UI;
using UnityEngine;

public enum CharacterChoice { Buster }

public abstract class Character
{
    // Gets the character name
    public abstract string name { get; }

    // References to the player and opponent are how we query / alter game state
    protected Player player;
    protected Player opponent;

    // Counter for how many throws each throw type should be disabled for
    private int rockDisabled = -1;
    private int paperDisabled = -1;
    private int scissorsDisabled = -1;

    // A struct representing the available options for a character during a
    // particular throw
    public struct ThrowOptions
    {
        public Throw rock;
        public Throw paper;
        public Throw scissors;
    }

    // These functions will be implemented in each character and return the
    // options available based on the current game state
    protected abstract Throw RockThrowOption();
    protected abstract Throw PaperThrowOption();
    protected abstract Throw ScissorsThrowOption();

    // HP related fields and properties
    private Slider healthSlider;
    private readonly int maxHp = 6;
    private int _hp;
    private int hp
    {
        get { return _hp; }
        set { _hp = value; healthSlider.value = value; }
    }

    // Constructor.  sets maxHP, configures the base UI, calls child method
    // to configure character-specific UI, and stores player and opponent
    // as a handle on the gamestate.
    public Character(Player p_player, Player p_opponent, RectTransform myUI)
    {
        _hp = maxHp;
        ConfigureBaseUI(myUI);
        ConfigureCharacterUI(myUI);
        player = p_player;
        opponent = p_opponent;
    }

    // Get the available throw options, filtering for disabled throws
    public ThrowOptions GetThrowOptions()
    {
        ThrowOptions options = new ThrowOptions();
        options.rock = rockDisabled <= 0 ? RockThrowOption() : null;
        options.scissors = scissorsDisabled <= 0 ? ScissorsThrowOption() : null;
        options.paper = paperDisabled <= 0 ? PaperThrowOption() : null;
        return options;
    }

    private void ConfigureBaseUI(RectTransform myUI)
    {
        healthSlider = myUI.Find("Health Bar/Health Slider")
            .GetComponent<Slider>();
        healthSlider.maxValue = maxHp;
        healthSlider.value = maxHp;
    }

    public bool isAlive() { return hp > 0; }
    public void Damage(int damage) { hp = hp < damage ? 0 : hp - damage; }
    protected virtual void ConfigureCharacterUI(RectTransform myUI) { }

    public void DisableThrowType(ThrowType throwType, int disableFor)
    {
        if(throwType == ThrowType.Rock)
            rockDisabled = disableFor;
        if(throwType == ThrowType.Paper)
            paperDisabled = disableFor;
        if(throwType == ThrowType.Scissors)
            scissorsDisabled = disableFor;
    }
}
