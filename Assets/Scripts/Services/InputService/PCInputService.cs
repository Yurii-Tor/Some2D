using MechingCards.Common;
using MechingCards.GameplayService;

namespace MechingCards.InputService {
	public class PCInputService : IInputService {
		public IInputController InputController { get; }

		public PCInputService() {
			InputController = new PCInputController();
		}
	}
}
