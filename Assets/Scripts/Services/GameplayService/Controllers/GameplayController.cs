using System;
using System.Collections.Generic;
using System.Linq;
using MechingCards.Common;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private Camera m_mainCamera;

        private Vector3Int? m_awaitingCard;
        private Action m_onGameFinished;

        private Action m_onTurn;
        private Action m_onMatch;
        private Action m_onFlip;
        private Action m_onDismatch;
        private Action m_onWin;
        private Action<Dictionary<Vector3Int, int>, Vector2Int> m_onSaveData;

        public void Initialize(GameplayData gameplayData, Dictionary<Vector3Int, int> saveData) {
            m_inputController = gameplayData.InputController;
            m_inputController.BlockInput();
            
            m_mainCamera = Camera.main;
            m_xSize = gameplayData.Columns;
            m_ySize = gameplayData.Rows;
            m_onGameFinished = gameplayData.OnGameFinished;
            m_onMatch = gameplayData.OnMatch;
            m_onTurn = gameplayData.OnTurn;
            m_onDismatch = gameplayData.OnDismatch;
            m_onFlip = gameplayData.OnFlip;
            m_onWin = gameplayData.OnWin;
            m_onSaveData = gameplayData.OnSave;
            
            CreateBoard();
            CameraUtils.SetupCamera(m_mainCamera, m_xSize, m_ySize, m_grid);
            ShowIntro();
        }

        public void Deinitialize() {
            foreach (var view in m_views) {
                Destroy(view.Value.gameObject);
            }
        }

        private void CreateBoard() {
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

            m_awaitingCard = null;
        }

        private void ShowIntro() {
            foreach (var view in m_views) {
                var card = view.Value;
                card.Reveal((() => {
                    card.HideWithDelay(3f, () => {
                        Debug.LogError("Initialized");
                        m_inputController.UnlockInput();
                    });
                }));
            }
        }

        private void Update() {
            if (m_inputController.IsLocked) {
                return;
            }

            if (!m_inputController.HasInput) {
                return;
            }

            var clickPosition = m_inputController.Position;
            var worldPosision = m_mainCamera.ScreenToWorldPoint(clickPosition);
            worldPosision.z = 0;

            var clickedCell = m_grid.WorldToCell(worldPosision);
            
            if (!m_views.TryGetValue(clickedCell, out var view)) {
                return;
            }

            if (!m_cellContent.TryGetValue(clickedCell, out var card)) {
                return;
            }

            if (card.IsLocked) {
                return;
            }
            card.Lock();

            var awaitningCard = m_awaitingCard;
            m_onFlip?.Invoke();
            view.Reveal(() => OnRevealed(clickedCell, awaitningCard));
            
            if (m_awaitingCard is not { }) {
                m_awaitingCard = clickedCell;
            } else {
                m_awaitingCard = null;
            }
        }

        private void OnRevealed(Vector3Int currentCell, Vector3Int? awaitingCardCell) {
            if (awaitingCardCell is not { }) {
                return; // just keep the card revealed
            }

            var acc = awaitingCardCell.Value;
            
            var currentCard = m_cellContent[currentCell];
            var currentView = m_views[currentCell];
            var awaitingCard = m_cellContent[acc];
            var awaitingView = m_views[acc];
            if (currentCard.ID == awaitingCard.ID) {
                currentView.DestroyWithDelay(1f, null);
                awaitingView.DestroyWithDelay(1f, () =>
                    AcquirePair(currentCell, acc));
            } else {
                currentView.HideWithDelay(1f, () => currentCard.Unlock());
                awaitingView.HideWithDelay(1f, () => awaitingCard.Unlock());
                m_onDismatch?.Invoke();
            }
            m_onTurn?.Invoke();
        }

        private void AcquirePair(Vector3Int cell1, Vector3Int cell2) {
            m_cellContent.Remove(cell1);
            m_cellContent.Remove(cell2);
            m_views.Remove(cell1);
            m_views.Remove(cell2);
            m_onMatch?.Invoke();
            
            if (m_views.Count == 0) { // meaning the last pair left
                m_onWin?.Invoke();
                m_onGameFinished?.Invoke();
            }
        }

        private void OnDestroy() {
            if (m_views.Count == 0) {
                m_onSaveData?.Invoke(null,Vector2Int.zero);
                return;
            }
            
            m_onSaveData?.Invoke(m_cellContent.ToDictionary(kvp => kvp.Key, kvp => kvp.Value is {} ? kvp.Value.ID : -1), new Vector2Int(m_xSize, m_ySize));
        }
    }
}
