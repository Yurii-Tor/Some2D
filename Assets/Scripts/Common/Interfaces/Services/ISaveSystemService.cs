using System;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace MechingCards.Common {
	public interface ISaveSystemService {
		void Initialize(Action<Dictionary<Vector3Int, int>, Vector2Int> callback);
		void Save(Dictionary<Vector3Int, int> data, Vector2Int size);
	}
}
