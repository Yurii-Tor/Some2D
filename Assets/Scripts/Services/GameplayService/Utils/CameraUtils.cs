using UnityEngine;

namespace MechingCards.GameplayService {
	public static class CameraUtils {
		public static void SetupCamera(Camera camera, int x, int y, Grid grid) {
			Vector3 centerPosition = CalculateGridCenter(x,y,grid);
			camera.transform.position =
				new Vector3(centerPosition.x, centerPosition.y, camera.transform.position.z);

			var orthographicSize = Mathf.Max(x, y) / 2f;
			camera.orthographicSize = orthographicSize;
		}
		
		private static Vector3 CalculateGridCenter(int x, int y, Grid grid) {
			var centerX = (x - 1) * .5f;
			var centerY = (y - 1) * .5f;
			return grid.GetCellCenterWorld(new Vector3Int(Mathf.RoundToInt(centerX), Mathf.RoundToInt(centerY), 0));
		}
	}
}