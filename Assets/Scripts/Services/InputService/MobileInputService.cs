using MechingCards.Common;

namespace MechingCards.InputService {
	public class MobileInputService : IInputService {
		public IInputController InputController { get; }

		public MobileInputService() {
			InputController = new MobileInputController();
		}
	}
}
