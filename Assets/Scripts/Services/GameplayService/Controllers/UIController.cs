using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace MechingCards.GameplayService {
    public class UIController : MonoBehaviour {

        [SerializeField] private TMP_Text m_MatchesValueLable;
        [SerializeField] private TMP_Text m_TurnsValueLable;
        [SerializeField] private Button m_ExitButton;

        public void Initialize(int matches, int turns, Action exitAction) {
            m_MatchesValueLable.text = matches.ToString();
            m_TurnsValueLable.text = turns.ToString();
            m_ExitButton.onClick.AddListener(() => exitAction?.Invoke());
        }
    }
}