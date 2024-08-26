using MechingCards.Common;
using UnityEngine;

namespace MechingCards.MenuService {
	public class MenuService : IMenuService {

		private MenuController m_menuController;
		private IGameplayService m_gameplayService;
		
		public MenuService(ISaveSystemService saveService, IGameplayService gameplayService) {
			m_gameplayService = gameplayService;
		}

		public void Initialize() {
			var menuController = Resources.Load("MenuController");
			var mgo = GameObject.Instantiate(menuController, null) as GameObject;
			m_menuController = mgo.GetComponent<MenuController>();
			m_menuController.Initialize(OnStart);
		}

		private void OnStart(int rows, int columns) {
			m_gameplayService.Initialize(rows, columns);
			GameObject.Destroy(m_menuController.gameObject);
		}
	}
}
