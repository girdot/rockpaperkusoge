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
        player1.Reset( Player.CharacterChoice.Buster, player2 );
        player2.Reset( Player.CharacterChoice.Buster, player1 );
        roundCounter.text = "Throw #: 1";
    }

    void Update()
    {
        if(player1.selectedThrow != null && player2.selectedThrow != null)
        {
            Throw.ResolveThrow( player1.selectedThrow, player2.selectedThrow );
            player1.selectedThrow = null;
            player2.selectedThrow = null;
            throwCounter++;
            if(!player1.character.isAlive()){
                throwCounter = 1;
                player1.Reset( Player.CharacterChoice.Buster, player2, false );
                player2.Reset( Player.CharacterChoice.Buster, player1, true );
            }
            if(!player2.character.isAlive()){
                throwCounter = 1;
                player1.Reset( Player.CharacterChoice.Buster, player1, true );
                player2.Reset( Player.CharacterChoice.Buster, player2, false );
            }
            roundCounter.text = string.Format( "Throw #: {0}", throwCounter );
        }
    }

    public void throwSelection(int throwNumber)
    {
        if( throwNumber > 0)
            player1.selectedThrow = player1.character.throws[throwNumber-1];
        else
            player2.selectedThrow = player2.character.throws[Mathf.Abs(throwNumber)-1];
    }
}

