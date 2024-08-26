using UnityEngine;

namespace MechingCards.GameplayService {
	public static class CameraUtils {
		public static void SetupCamera(Camera camera, int x, int y, Grid grid) {
			Vector3 centerPosition = CalculateGridCenter(x,y,grid);
			camera.transform.position =
				new Vector3(centerPosition.x, centerPosition.y, camera.transform.position.z);

			// Calculate the required orthographic size
			float verticalSize = x * grid.cellSize.y * 0.5f;
			float horizontalSize = (x * grid.cellSize.x * 0.5f) / camera.aspect;
    
			camera.orthographicSize = Mathf.Max(verticalSize, horizontalSize);
		}
		
		private static Vector3 CalculateGridCenter(int x, int y, Grid grid) {
			var centerX = (x - 1) * .5f;
			var centerY = (y - 1) * .5f;
			
			// Convert to cell coordinates
			Vector3Int cellCoordinates = new Vector3Int(
				Mathf.FloorToInt(centerX),
				Mathf.FloorToInt(centerY),
				0
			);

			// Get the world position of the cell
			Vector3 cellCenter = grid.GetCellCenterWorld(cellCoordinates);

			// For even dimensions, adjust by half a cell size
			if (x % 2 == 0) {
				cellCenter.x += grid.cellSize.x * 0.5f;
			}

			if (y % 2 == 0) {
				cellCenter.y += grid.cellSize.y * 0.5f;
			}

			return cellCenter;
		}
	}
}