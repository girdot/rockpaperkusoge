using UnityEngine;
using TMPro;
using System;

public class RPKChoice
{
    private RPKChoice(string p_toString) { _toString = p_toString; }
    public static readonly RPKChoice Rock = new RPKChoice("Rock");
    public static readonly RPKChoice Paper = new RPKChoice("Paper");
    public static readonly RPKChoice Scissors = new RPKChoice("Scissors");
    private string _toString;

    public static bool operator >(RPKChoice a, RPKChoice b)
    {
        if (a == RPKChoice.Rock && b == RPKChoice.Scissors) return true;
        if (a == RPKChoice.Paper && b == RPKChoice.Rock) return true;
        if (a == RPKChoice.Scissors && b == RPKChoice.Paper) return true;
        return false;
    }

    public static bool operator <(RPKChoice a, RPKChoice b)
    {
        return !(a > b) && !(a == b);
    }

    public override string ToString()
    {
        return _toString;
    }
}

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
        player1.Reset(RPKCharSelect.Buster, player2);
        player2.Reset(RPKCharSelect.Buster, player1);
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
                player1.Reset(RPKCharSelect.Buster, player2, player1.character.isAlive());
                player2.Reset(RPKCharSelect.Buster, player1, player2.character.isAlive());
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
