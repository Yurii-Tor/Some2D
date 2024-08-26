using UnityEngine;

namespace MechingCards.Common {
	public interface IInputController {
		bool HasInput { get; }
		Vector3 Position { get; }
	}
}
