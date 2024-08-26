using System;

namespace MechingCards.Common {
	public interface IGameplayService {
		void Initialize(int rows, int columns, Action onGameFinished);
	}
}
