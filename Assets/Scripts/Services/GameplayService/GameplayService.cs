using MechingCards.Common;
using UnityEngine;

namespace MechingCards.GameplayService {
	public class GameplayService : IGameplayService {
		private GameplayController m_gameplayController;
		public GameplayService(IInputService inputService) {
			
		}

		public void Initialize() {
			var gameplayController = Resources.Load("GameplayController");
			var go = GameObject.Instantiate(gameplayController, null) as GameObject;
			m_gameplayController = go.GetComponent<GameplayController>();
		}
	}
}
