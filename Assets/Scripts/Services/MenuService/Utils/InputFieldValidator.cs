using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechingCards.MenuService {
    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldValidator : MonoBehaviour {

        [SerializeField] private int m_minValue;
        [SerializeField] private int m_maxValue;

        private TMP_InputField m_inputField;
        
        private void Awake() {
            m_inputField = GetComponent<TMP_InputField>(); 
            m_inputField.onValueChanged.AddListener(ValidateInput);
            m_inputField.onEndEdit.AddListener(OnEndEdit);
        }
        
        
        private void ValidateInput(string input) {
            if (int.TryParse(input, out int value)) {
                if (value < m_minValue || value > m_maxValue) {
                    m_inputField.text = Mathf.Clamp(value, m_minValue, m_maxValue).ToString();
                }
            } else if (!string.IsNullOrEmpty(input)) {
                m_inputField.text = m_minValue.ToString();
            }
        }

        private void OnEndEdit(string input) {
            if (string.IsNullOrEmpty(input)) {
                m_inputField.text = m_minValue.ToString();
            }
        }

        public int GetValue() {
            return int.Parse(m_inputField.text);
        }
    }
}