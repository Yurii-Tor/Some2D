using MechingCards.Common;
using UnityEngine;

namespace MechingCards.GameplayService {
	public class GameplayService : IGameplayService {
		private GameplayController m_gameplayController;
		private UIController m_uiController;
		private IInputService m_inputService;
		public GameplayService(IInputService inputService) {
			m_inputService = inputService;
		}

		public void Initialize(int rows, int columns) {
			var gameplayController = Resources.Load("GameplayController");
			var gpgo = GameObject.Instantiate(gameplayController, null) as GameObject;
			m_gameplayController = gpgo.GetComponent<GameplayController>();
			m_gameplayController.Initialize(rows,columns, m_inputService.InputController);
			
			var uiController = Resources.Load("UIController");
			var uigo = GameObject.Instantiate(uiController, null) as GameObject;
			m_uiController = uigo.GetComponent<UIController>();
			m_uiController.Initialize(0, 0, () => {
				m_gameplayController.Deinitialize();
				GameObject.Destroy(gpgo);
				GameObject.Destroy(uigo);
			});
		}
	}
}
