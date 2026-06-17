using UnityEngine;
using TMPro;
using System;
using RPKCharacters;

public class RPKManager : MonoBehaviour
{
    // Inspector assigned
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private TMP_Text roundCounter;
    public static event Action ThrowFinished;

    private int throwCounter = 1;

    void Start()
    {
        player1.Reset(RPKCharSamurai.CharName, player2);
        player2.Reset(RPKCharBuster.CharName, player1);
        roundCounter.text = "Throw #: 1";
    }

    void Update()
    {
        if (player1.throwSelection != null && player2.throwSelection != null)
        {
            RPKThrow.ResolveThrow(player1, player2);
            if (!player1.character.isAlive() || !player2.character.isAlive())
            {
                throwCounter = 1;
                player1.Reset(RPKCharSamurai.CharName, player2, player1.character.isAlive());
                player2.Reset(RPKCharBuster.CharName, player1, player2.character.isAlive());
            }
            else
            {
                throwCounter++;
                ThrowFinished();
            }
            roundCounter.text = string.Format("Throw #: {0}", throwCounter);
        }
    }
}
