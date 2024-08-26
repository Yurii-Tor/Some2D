using MechingCards.Common;
using UnityEngine;

namespace MechingCards.GameplayService {
    public class PCInputController : IInputController {
        public bool HasInput => Input.GetMouseButtonUp(0);
        public Vector3 Position => Input.mousePosition;
    }
}