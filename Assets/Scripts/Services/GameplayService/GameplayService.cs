using MechingCards.Common;
using UnityEngine;

namespace MechingCards.GameplayService {
	public class GameplayService : IGameplayService {
		private GameplayController m_gameplayController;
		private IInputService m_inputService;
		public GameplayService(IInputService inputService) {
			m_inputService = inputService;
		}

		public void Initialize() {
			var gameplayController = Resources.Load("GameplayController");
			var go = GameObject.Instantiate(gameplayController, null) as GameObject;
			m_gameplayController = go.GetComponent<GameplayController>();
			m_gameplayController.Initialize(3,3, m_inputService.InputController);
		}
	}
}
