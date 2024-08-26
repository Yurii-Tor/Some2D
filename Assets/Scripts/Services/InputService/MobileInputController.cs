using MechingCards.Common;
using UnityEngine;

namespace MechingCards.InputService {
    public class MobileInputController : IInputController {
        public bool HasInput => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
        public bool IsLocked { get; private set; }
        public Vector3 Position => Input.GetTouch(0).position;
        
        public void BlockInput() {
            IsLocked = true;
        }

        public void UnlockInput() {
            IsLocked = false;
        }
    }
}