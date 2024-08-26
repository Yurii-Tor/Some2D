using System;
using MechingCards.Common;

namespace MechingCards.GameplayService {
    public struct GameplayData {
        public int Columns;
        public int Rows;
        public IInputController InputController;
        public Action OnGameFinished;
        public Action OnMatch;
        public Action OnTurn;
    }
}