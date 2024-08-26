using System;
using System.Collections.Generic;
using UnityEngine;

namespace MechingCards.Common {
	public interface IGameplayService {
		void Initialize(int rows, int columns, Dictionary<Vector3Int, int> saveData, Action onGameFinished);
	}
}
