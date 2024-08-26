using System.Collections.Generic;

namespace MechingCards.SaveSystemService {
	[System.Serializable]
	public class SerializableDictionary<TKey, TValue> {
		public List<TKey> keys = new List<TKey>();
		public List<TValue> values = new List<TValue>();

		public SerializableDictionary(Dictionary<TKey, TValue> dictionary) {
			foreach (var kvp in dictionary) {
				keys.Add(kvp.Key);
				values.Add(kvp.Value);
			}
		}

		public Dictionary<TKey, TValue> ToDictionary() {
			var dictionary = new Dictionary<TKey, TValue>();
			for (int i = 0; i < keys.Count; i++) {
				dictionary.Add(keys[i], values[i]);
			}

			return dictionary;
		}
	}
}