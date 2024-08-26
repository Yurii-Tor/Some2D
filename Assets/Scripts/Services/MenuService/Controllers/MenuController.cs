using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechingCards.MenuService {
	public class MenuController : MonoBehaviour {
		[SerializeField] private Button m_startButton;
		[SerializeField] private Button m_continueButton;
		[SerializeField] private TMP_InputField m_rows;
		[SerializeField] private TMP_InputField m_columns;

		public void Initialize(Vector2Int size, Action<int, int> onStartButton) {
			m_rows.text = $"{size.x}";
			m_columns.text = $"{size.y}";
			m_startButton.onClick.AddListener(() => onStartButton(int.Parse(m_rows.text), int.Parse(m_columns.text)));
			m_continueButton.onClick.AddListener(() => onStartButton(int.Parse(m_rows.text), int.Parse(m_columns.text)));
			
		}
	}
}