using System;
using System.Collections.Generic;
using MechingCards.Common;
using UnityEngine;

namespace MechingCards.MenuService {
	public class MenuService : IMenuService {

		private MenuController m_menuController;
		private IGameplayService m_gameplayService;
		
		public MenuService(ISaveSystemService saveService, IGameplayService gameplayService) {
			m_gameplayService = gameplayService;
		}

		public void Initialize(Dictionary<Vector3Int, int> data, Vector2Int size) {
			var menuController = Resources.Load("MenuController");
			var mgo = GameObject.Instantiate(menuController, null) as GameObject;
			m_menuController = mgo.GetComponent<MenuController>();
			m_menuController.Initialize(size, OnStart);
		}

		private void OnStart(int rows, int columns) {
			m_gameplayService.Initialize(rows, columns, null, () => Initialize(null, Vector2Int.zero));
			GameObject.Destroy(m_menuController.gameObject);
		}

		private void OnContinue(int rows, int columns, Dictionary<Vector3Int, int> data) {
			m_gameplayService.Initialize(rows, columns, data, () => Initialize(null, Vector2Int.zero));
			GameObject.Destroy(m_menuController.gameObject);
		}
	}
}
