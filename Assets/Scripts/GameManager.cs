using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameState currentState;

    [SerializeField] private GameObject[] m_cells;
    [SerializeField] private int m_countCells;
    [SerializeField] private SpawnerCell spawner;
    [SerializeField] private AudioSource m_audioS;
    [SerializeField] private GameObject m_winBoard;
    [SerializeField] private AudioClip m_winSound;
    [SerializeField] private TextMeshProUGUI m_textWinner;

    private string[] m_board = new string[9];
    private int m_count = 0;    

    private void Start()
    {
        m_cells = new GameObject[m_countCells];
        m_cells = spawner.SpawnCells(m_countCells, this);
    }

    public void OnCellClicked(int index)
    {
        if (currentState == GameState.GameOver || m_board[index] == "X" || m_board[index] == "O")
            return;

        string symbol = (currentState == GameState.PlayerXTurn) ? "X" : "O";

        m_count++;

        Debug.Log($"curret State {symbol}");

        m_board[index] = symbol;

        if (CheckWin(symbol))
        {
            Debug.Log("Win");
            currentState = GameState.GameOver;
        }
        else if (m_countCells <= m_count)
        {
            Debug.Log("End");
            currentState = GameState.GameOver;
            Winner(false);
        }
        else
        {
            currentState = (currentState == GameState.PlayerXTurn) ?
                          GameState.PlayerOTurn : GameState.PlayerXTurn;
        }
        
        m_audioS.Play();
    }

    bool CheckWin(string symbol)
    { 
        int[,] winPatterns = {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Горизонтали
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Вертикали
            {0, 4, 8}, {2, 4, 6}              // Диагонали
        };

        for (int i = 0; i < winPatterns.GetLength(0); i++)
        {
            if (m_board[winPatterns[i, 0]] == symbol &&
                m_board[winPatterns[i, 1]] == symbol &&
                m_board[winPatterns[i, 2]] == symbol)
            {
                Winner(true);
                return true;
            }
        }
        return false;
    }

    bool IsBoardFull()
    {
        foreach (string cell in m_board)
        {
            if (cell != "X" || cell != "O")
                return false;
        }
        return true;
    }

    private void Winner(bool isWin)
    {
        m_audioS.clip = m_winSound;
        m_audioS.Play();
        m_winBoard.SetActive(true);
        if (!isWin)
        {
            m_textWinner.text = "Draw";
            return;
        }
        if(currentState == GameState.PlayerXTurn)
        {
            m_textWinner.text = "Winner X";
        }
        else
        {
            m_textWinner.text = "Winner O";
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
