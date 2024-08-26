using MechingCards.Common;
using UnityEngine;

namespace MechingCards.InputService {
    public class MobileInputController : IInputController {
        public bool HasInput => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
        public Vector3 Position => Input.GetTouch(0).position;
    }
}