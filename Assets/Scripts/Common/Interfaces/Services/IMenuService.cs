using System;
using System.Collections.Generic;
using UnityEngine;

namespace MechingCards.Common {
	public interface IMenuService {
		void Initialize(Dictionary<Vector3Int, int> data, Vector2Int size);
	}
}
