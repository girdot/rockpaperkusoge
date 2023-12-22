using UnityEngine;
using TMPro;

public class RPKManager : MonoBehaviour
{
    // Inspector assigned
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private TMP_Text roundCounter;

    private int throwCounter = 1;

    void Start()
    {
        player1.Reset(CharacterChoice.Buster, player2);
        player2.Reset(CharacterChoice.Buster, player1);
        roundCounter.text = "Throw #: 1";
    }

    void Update()
    {
        if (player1.selectedThrow != null && player2.selectedThrow != null)
        {
            Throw.ResolveThrow(player1.selectedThrow, player1.isParrying, player2.selectedThrow, player2.isParrying);
            player1.selectedThrow = null;
            player2.selectedThrow = null;
            throwCounter++;
            if (!player1.character.isAlive())
            {
                throwCounter = 1;
                player1.Reset(CharacterChoice.Buster, player2, false);
                player2.Reset(CharacterChoice.Buster, player1, true);
            }
            if (!player2.character.isAlive())
            {
                throwCounter = 1;
                player1.Reset(CharacterChoice.Buster, player1, true);
                player2.Reset(CharacterChoice.Buster, player2, false);
            }
            roundCounter.text = string.Format("Throw #: {0}", throwCounter);
        }
    }

    public void throwSelection(int throwNumber)
    {
        switch (throwNumber)
        {
            case 1:
                player1.selectedThrow = player1.character.GetThrowOptions().rock;
                player1.isParrying = Input.GetKey(KeyCode.LeftShift);
                break;
            case 2:
                player1.selectedThrow = player1.character.GetThrowOptions().paper;
                player1.isParrying = Input.GetKey(KeyCode.LeftShift);
                break;
            case 3:
                player1.selectedThrow = player1.character.GetThrowOptions().scissors;
                player1.isParrying = Input.GetKey(KeyCode.LeftShift);
                break;
            case -1:
                player2.selectedThrow = player2.character.GetThrowOptions().rock;
                player2.isParrying = Input.GetKey(KeyCode.LeftShift);
                break;
            case -2:
                player2.selectedThrow = player2.character.GetThrowOptions().paper;
                player2.isParrying = Input.GetKey(KeyCode.LeftShift);
                break;
            case -3:
                player2.selectedThrow = player2.character.GetThrowOptions().scissors;
                player2.isParrying = Input.GetKey(KeyCode.LeftShift);
                break;
        }
    }
}

