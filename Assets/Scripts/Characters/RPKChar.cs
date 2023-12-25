using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using RPKCharacters;

public enum RPKCharSelect { Buster }

public abstract class RPKChar
{
    public abstract string name { get; }
    protected abstract int maxHp { get; }
    protected Dictionary<string, RPKThrow> throwLibrary = new Dictionary<string, RPKThrow>();
    protected Dictionary<RPKChoice, RPKThrow> throwSelection = new Dictionary<RPKChoice, RPKThrow>
        { {RPKChoice.Rock, null},{RPKChoice.Paper, null},{RPKChoice.Scissors, null} };
    private Dictionary<RPKChoice, int> disabledTimers = new Dictionary<RPKChoice, int>()
        { {RPKChoice.Rock, 0}, {RPKChoice.Paper, 0}, {RPKChoice.Scissors, 0} };
    private Slider _healthSlider;
    private int hp;
    protected RectTransform _myUI;

    protected RPKChar(Player p_player, Player p_opponent, RectTransform myUI)
    {
        hp = maxHp;
        _myUI = myUI;
        InitCharacterUI();
        RPKManager.ThrowFinished += PostThrowUpdate;
    }

    public static RPKChar SelectRPKChar(
            RPKCharSelect charChoice,
            Player p_player,
            Player p_opponent,
            RectTransform myUI)
    {
        switch (charChoice)
        {
            case RPKCharSelect.Buster:
                return new RPKCharBuster(p_player, p_opponent, myUI);
        }
        return null;
    }

    private void InitCharacterUI()
    {
        _healthSlider = _myUI.Find("Health Bar/Health Slider").GetComponent<Slider>();
        _healthSlider.maxValue = maxHp;
        _healthSlider.value = maxHp;
        InitCharacterSpecificUI();
    }

    public void PostThrowUpdate()
    {
        UpdateCharacterUI();
        disabledTimers[RPKChoice.Rock]--;
        disabledTimers[RPKChoice.Paper]--;
        disabledTimers[RPKChoice.Scissors]--;
    }

    public void UpdateCharacterUI()
    {
        _healthSlider.value = hp;
        _myUI.Find("Throw Select/Rock Button").gameObject.SetActive(
                !throwSelection[RPKChoice.Rock].isDisabled());
        _myUI.Find("Throw Select/Paper Button").gameObject.SetActive(
                !throwSelection[RPKChoice.Paper].isDisabled());
        _myUI.Find("Throw Select/Scissors Button").gameObject.SetActive(
                !throwSelection[RPKChoice.Scissors].isDisabled());
    }

    protected virtual void InitCharacterSpecificUI() { }
    protected virtual void UpdateCharacterSpecificUI() { }
    public void RegisterThrow(RPKThrow p_throw) { throwLibrary[p_throw.name] = p_throw; }
    public RPKThrow GetThrow(string throwName) { return throwLibrary[throwName]; }
    public RPKThrow selectThrow(RPKChoice choice) { return throwSelection[choice]; }
    public bool isAlive() { return hp > 0; }
    public void Damage(int damage) { hp = hp < damage ? 0 : hp - damage; }
    public void Disable(RPKChoice throwType, int timer = 1) { disabledTimers[throwType] = timer; }
    public bool isDisabled(RPKChoice throwType) { return disabledTimers[throwType] > 0; }
}
