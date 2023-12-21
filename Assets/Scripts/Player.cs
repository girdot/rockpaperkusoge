using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Inspector assigned
    [SerializeField] private RectTransform characterUI;

    [HideInInspector] public Character character { get; private set; }
    [HideInInspector] public Throw selectedThrow;
    private Image victoryCrown;

    void Start()
    {
        victoryCrown = characterUI.Find("Victory Crown").GetComponent<Image>();
    }

    public void Reset( CharacterChoice p_char, Player opponent,
            bool isWinner = false)
    {
        switch (p_char)
        {
            case CharacterChoice.Buster:
                character = new CharBuster(this, opponent, characterUI);
                break;
        }
        victoryCrown.enabled = isWinner;
    }
}

