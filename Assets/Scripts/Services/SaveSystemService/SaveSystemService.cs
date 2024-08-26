using System;
using System.Collections.Generic;
using System.IO;
using MechingCards.Common;
using UnityEngine;

namespace MechingCards.SaveSystemService {
	public class SaveSystemService : ISaveSystemService {
		private const string FILE_EXTENSION = ".json";
		private const string DATA = "data";
		private string FileFullPath => $"{CacheConfig.PersistentDataPath}{FILE_EXTENSION}";
		public void Initialize(Action<Dictionary<Vector3Int, int>, Vector2Int> callback) {
			TryLoad(out var data, out var size);
			callback?.Invoke(data, size);
		}

		public void Save(Dictionary<Vector3Int, int> data, Vector2Int size) {
			if (data is not { }) {
				PlayerPrefs.DeleteKey(DATA);
				return;
			}
			var model = new SaveModel(data, size);
			
			PlayerPrefs.SetString(DATA, JsonUtility.ToJson(model));
			PlayerPrefs.Save();
		}

		public bool TryLoad(out Dictionary<Vector3Int, int> data, out Vector2Int size) {
			var dataString = PlayerPrefs.GetString(DATA, null);
			if (dataString is not { }) {
				data = null;
				size = Vector2Int.zero;
				return false;
			}

			var model = JsonUtility.FromJson<SaveModel>(dataString);
			data = model.GetGameData();
			size = model.GetSize();
			return true;
		}
	}
}
