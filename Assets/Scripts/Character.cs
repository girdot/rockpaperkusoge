using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterChoice { Buster }

public abstract class Character
{
    protected string name;
    private Slider healthSlider;
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
        ConfigureBaseUI(myUI);
        ConfigureCharacterUI(myUI);
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
    protected abstract void ConfigureThrows(Player p_player, Player p_opponent);
}
