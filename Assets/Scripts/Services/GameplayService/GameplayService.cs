using System;
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

		public void Initialize(int rows, int columns, Action onGameFinished) {
			var gameplayController = Resources.Load("GameplayController");
			var gpgo = GameObject.Instantiate(gameplayController, null) as GameObject;
			m_gameplayController = gpgo.GetComponent<GameplayController>();
			
				var uiController = Resources.Load("UIController");
			var uigo = GameObject.Instantiate(uiController, null) as GameObject;
			m_uiController = uigo.GetComponent<UIController>();
			
			
			Action exitAction = () => {
				m_gameplayController.Deinitialize();
				GameObject.Destroy(gpgo);
				GameObject.Destroy(uigo);
				onGameFinished?.Invoke();
			};

			
			m_uiController.Initialize(0, 0, exitAction);
			
			var data = new GameplayData() {
				Rows = rows,
				Columns = columns,
				InputController = m_inputService.InputController,
				OnMatch = m_uiController.Match,
				OnTurn = m_uiController.Turn,
				OnGameFinished = exitAction
			};
			
			m_gameplayController.Initialize(data);
		}
	}
}
