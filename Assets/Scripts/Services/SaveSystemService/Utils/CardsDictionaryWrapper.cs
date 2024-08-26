using System.Collections.Generic;
using UnityEngine;

namespace MechingCards.SaveSystemService {
	[System.Serializable]
	public class CardsDictionaryWrapper {
		public SerializableDictionary<Vector3Int, int> m_cardsDictionary;

		public CardsDictionaryWrapper(Dictionary<Vector3Int, int> dictionary) {
			m_cardsDictionary = new SerializableDictionary<Vector3Int, int>(dictionary);
		}
		
		public Dictionary<Vector3Int, int> ToDictionary() {
			return m_cardsDictionary != null ? m_cardsDictionary.ToDictionary() : new Dictionary<Vector3Int, int>();
		}
	}
}