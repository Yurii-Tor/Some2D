using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace MechingCards.GameplayService {
    public class UIController : MonoBehaviour {

        [SerializeField] private TMP_Text m_MatchesValueLable;
        [SerializeField] private TMP_Text m_TurnsValueLable;
        [SerializeField] private Button m_ExitButton;

        private int m_turns;
        private int m_matches;
        
        public void Initialize(int matches, int turns, Action exitAction) {
            m_MatchesValueLable.text = matches.ToString();
            m_TurnsValueLable.text = turns.ToString();
            m_ExitButton.onClick.AddListener(() => exitAction?.Invoke());
            m_turns = turns;
            m_matches = matches;
        }

        public void Match() {
            m_MatchesValueLable.text = (++m_matches).ToString();
        }
        
        public void Turn() {
            m_TurnsValueLable.text = (++m_turns).ToString();
        }
    }
}