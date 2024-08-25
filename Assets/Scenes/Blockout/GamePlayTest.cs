using System.Collections.Generic;
using UnityEngine;

public class GamePlayTest : MonoBehaviour{
    
    [SerializeField] private Grid m_grid;

    [SerializeField]
    private CardsMapping m_cardsMapping;

    [SerializeField]
    private GameObject m_cardPrefab;

    private int m_xSize = 2;
    private int m_ySize = 3;

    private Dictionary<int, Sprite> m_idMap = new();
        
    private Dictionary<Vector3Int, Card> m_cellContent = new();
    // Start is called before the first frame update
    void Start() {
        var count = m_xSize * m_ySize / 2;
        
        // get random sprites
        var indices = new List<int>();
        for (int i = 0; i < count; ++i) {
            int id;
            do {
                id = Random.Range(0, count);
            } while (indices.Contains(id));
            indices.Add(id);
        }

        // create cells list
        var cells = new List<Vector3Int>();
        for (int i = 0; i < m_xSize; ++i) {
            for (int j = 0; j < m_ySize; ++j) {
                cells.Add(new Vector3Int(i, j));
            }
        }
        
        // create cards
        var cards = new List<Card>();
        for (int i = count - 1; i >= 0; --i) { // backward since it's better for removing from the list
            var index = indices[i];
            cards.Add(new Card(index));
            cards.Add(new Card(index));
            indices.Remove(index);
        }

        if (m_xSize % 2 == 1 && m_ySize % 2 == 1) {
            cards.Add(null);
        }

        cards.Shuffle();

        count = cells.Count;
        for (int i = 0; i < count; i++) {
            m_cellContent.Add(cells[i], cards[i]);
        }

        foreach (var cell in m_cellContent) {
            var go = Instantiate(m_cardPrefab, m_grid.GetCellCenterWorld(cell.Key), Quaternion.identity);
            var renderer = go.GetComponent<SpriteRenderer>();
            renderer.sprite = m_cardsMapping.SpritesList[cell.Value.ID];
        }
    }

}
