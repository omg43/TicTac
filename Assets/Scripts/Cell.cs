using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Sprite m_spriteCircle, m_spriteCross;
    [SerializeField] private Button m_buttonCell;
    [SerializeField] private Image m_image;
    [SerializeField] private GameObject m_icon;
    
    private GameManager m_gameManager;
    private int m_id;

    public void Instanstate(int id, GameManager gm)
    {
        m_gameManager = gm;
        m_id = id;
        m_buttonCell.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        m_gameManager.OnCellClicked(m_id);
        m_icon.SetActive(true);
        if (m_gameManager.currentState == GameState.PlayerXTurn)
        {
            m_image.sprite = m_spriteCross;
        }
        else
        {
            m_image.sprite = m_spriteCircle;
        }
    }
}
