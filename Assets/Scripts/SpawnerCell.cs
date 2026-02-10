using UnityEngine;

public class SpawnerCell : MonoBehaviour
{
    [SerializeField] private GameObject m_cellsPref;
    private GameObject[] cellsSpawn; 
    public GameObject[] SpawnCells(int countCells, GameManager gameManager)
    {
        cellsSpawn = new GameObject[countCells];

        for(int i = 0; i < countCells; i++)
        {
            GameObject cell = Instantiate(m_cellsPref, gameObject.transform);
            cellsSpawn[i] = cell;
            cell.GetComponent<Cell>().Instanstate(i ,gameManager);
            Debug.Log($"New cell {i}");
        }
        return cellsSpawn;
    }
}
