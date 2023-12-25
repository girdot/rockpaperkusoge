using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Inspector assigned
    [SerializeField] private RectTransform characterUI;
    [HideInInspector] public RPKChoice throwSelection {get; private set;}
    [HideInInspector] public bool isParrying {get; private set;}
    [HideInInspector] public RPKChar character { get; private set; }
    private Image victoryCrown;

    void Start() { victoryCrown = characterUI.Find("Victory Crown").GetComponent<Image>(); }

    public void Reset(RPKCharSelect p_char, Player opponent, bool isWinner = false)
    {
        character = RPKChar.SelectRPKChar(p_char, this, opponent, characterUI);
        victoryCrown.enabled = isWinner;
        throwSelection = null;
        RPKManager.ThrowFinished += PostThrowUpdate;
    }

    public void selectThrow(int choice)
    {
        isParrying = Input.GetKey(KeyCode.LeftShift);
        if (choice == 1)
            throwSelection = RPKChoice.Rock;
        if (choice == 2)
            throwSelection = RPKChoice.Paper;
        if (choice == 3)
            throwSelection = RPKChoice.Scissors;
    }

    public RPKThrow GetSelectedThrow( ){ return character.selectThrow( throwSelection  ); }

    private void PostThrowUpdate() { throwSelection = null; }
}
