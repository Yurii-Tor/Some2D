using UnityEngine;

namespace MechingCards.Common {
	public interface IInputController {
		bool HasInput { get; }
		bool IsLocked { get; }
		Vector3 Position { get; }
		void BlockInput();
		void UnlockInput();
	}
}
