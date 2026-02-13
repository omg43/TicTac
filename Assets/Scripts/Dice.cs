using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private int m_diceRate; //1-20
    private int currentDice;

    public int RandomDice()
    {
        return Random.Range(0, m_diceRate);
    }
}
