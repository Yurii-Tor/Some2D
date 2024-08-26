using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechingCards.MenuService {
	public class MenuController : MonoBehaviour {
		[SerializeField] private Button m_startButton;
		[SerializeField] private TMP_InputField m_rows;
		[SerializeField] private TMP_InputField m_columns;

		public void Initialize(Action<int, int> onStartButton) {
			m_rows.text = "5";
			m_columns.text = "5";
			m_startButton.onClick.AddListener(() => onStartButton(int.Parse(m_rows.text), int.Parse(m_columns.text)));
		}
	}
}