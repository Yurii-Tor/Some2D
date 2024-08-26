using System.Collections.Generic;
using MechingCards.Common;
using UnityEngine;

namespace MechingCards.GameplayService {
    public class GameplayController : MonoBehaviour {

        [SerializeField] private Grid m_grid;

        [SerializeField] private CardsMapping m_cardsMapping;

        [SerializeField] private GameObject m_cardPrefab;

        private int m_xSize = 5;
        private int m_ySize = 5;

        private Dictionary<int, Sprite> m_idMap = new();

        private Dictionary<Vector3Int, Card> m_cellContent = new();

        private Dictionary<Vector3Int, CardView> m_views = new();

        private IInputController m_inputController;

        public void Initialize(int columns, int rows, IInputController inputController) {
            m_inputController = inputController;
            
            // TODO: block inputs here
            m_xSize = columns;
            m_ySize = rows;
            
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
            for (int i = count - 1; i >= 0; --i) {
                // backward since it's better for removing from the list
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
                var card = cell.Value;
                if (card is not { }) {
                    continue; // skip the empty cell
                }
                var go = Instantiate(m_cardPrefab, m_grid.GetCellCenterWorld(cell.Key), Quaternion.identity);

                var view = go.GetComponent<CardView>();

                if (view is not { }) {
                    Debug.LogError($"{nameof(GameplayController)} -> {nameof(Initialize)} :" +
                                   $"Couldn't find a the {nameof(CardView)} component in the card prefab");
                    return;
                }

                var sprite = m_cardsMapping.SpritesList[card.ID];

                m_views.Add(cell.Key, view);
                view.Initialize(m_cardsMapping.BackSideSprite, sprite);
            }
            
            ShowIntro();
        }

        private void ShowIntro() {
            foreach (var view in m_views) {
                var card = view.Value;
                card.Reveal((() => {
                    card.HideWithDelay(3f, () => {
                        Debug.LogError("Done");
                        //TODO: unlock inputs here
                    });
                }));
            }
            
        }

        private void Update() {
            if (m_inputController.HasInput) {
                // 0 is for left mouse button or first touch
                var mainCamera = Camera.main;
                var clickPosition = m_inputController.Position;
                var worldPosision = mainCamera.ScreenToWorldPoint(clickPosition);
                worldPosision.z = 0;

                var clickedCell = m_grid.WorldToCell(worldPosision);
                if (!m_views.TryGetValue(clickedCell, out var card)) {
                    return;
                }
                card.Reveal((() => {
                    card.HideWithDelay(1f, () => Debug.LogError("Done"));
                }));
            }
        }
    }
}
