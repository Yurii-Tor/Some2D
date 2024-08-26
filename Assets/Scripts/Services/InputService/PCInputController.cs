using MechingCards.Common;
using UnityEngine;

namespace MechingCards.GameplayService {
    public class PCInputController : IInputController {
        public bool HasInput => Input.GetMouseButtonUp(0);
        public bool IsLocked { get; private set; }
        public Vector3 Position => Input.mousePosition;
        
        public void BlockInput() {
            IsLocked = true;
        }

        public void UnlockInput() {
            IsLocked = false;
        }
    }
}