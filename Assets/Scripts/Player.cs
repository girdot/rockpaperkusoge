using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // Inspector assigned
    [SerializeField] private RectTransform characterUI;

    [HideInInspector] public Character character { get; private set; }
    [HideInInspector] public Throw selectedThrow;
    private Image victoryCrown;
    public enum CharacterChoice { Buster }

    void Start()
    {
        victoryCrown = characterUI.Find("Victory Crown").GetComponent<Image>();
    }

    public void Reset(
            CharacterChoice p_char,
            Player opponent,
            bool isWinner = false)
    {
        switch (p_char)
        {
            case CharacterChoice.Buster:
                character = new Character(this, opponent, characterUI);
                break;
        }
        victoryCrown.enabled = isWinner;
    }
}

public class Character
{
    private Slider healthSlider;
    private readonly string name = "Buster";
    private readonly int maxHp = 6;
    private int _hp;
    private int hp
    {
        get { return _hp; }
        set { _hp = value; healthSlider.value = value; }
    }
    public List<Throw> throws { get; private set; } = new List<Throw>();

    public Character(Player p_player, Player p_opponent, RectTransform myUI)
    {
        _hp = maxHp;
        ConfigureThrows(p_player, p_opponent);
        ConfigureUI(myUI);
    }

    private void ConfigureThrows(Player p_player, Player p_opponent)
    {
        throws.Add(new Throw(Throw.ThrowType.Rock, p_player, p_opponent));
        throws.Add(new Throw(Throw.ThrowType.Paper, p_player, p_opponent));
        throws.Add(new Throw(Throw.ThrowType.Scissors, p_player, p_opponent));
    }

    private void ConfigureUI(RectTransform myUI)
    {
        healthSlider = myUI.Find("Health Bar/Health Slider")
            .GetComponent<Slider>();
        healthSlider.maxValue = maxHp;
        healthSlider.value = maxHp;
    }

    public bool isAlive() { return hp > 0; }

    public void Damage(int damage) { hp = hp < damage ? 0 : hp - damage; }
}
