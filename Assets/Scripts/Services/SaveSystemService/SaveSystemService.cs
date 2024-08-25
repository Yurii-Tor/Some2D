using System;
using MechingCards.Common;

namespace MechingCards.SaveSystemService {
	public class SaveSystemService : ISaveSystemService {
		public void Initialize(Action callback) {
			callback?.Invoke();
		}
	}
}
