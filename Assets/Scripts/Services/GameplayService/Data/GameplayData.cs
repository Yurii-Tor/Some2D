using System;
using System.Collections.Generic;
using MechingCards.Common;
using UnityEngine;

namespace MechingCards.GameplayService {
    public struct GameplayData {
        public int Columns;
        public int Rows;
        public IInputController InputController;
        public Action OnGameFinished;
        public Action OnMatch;
        public Action OnTurn;
        public Action OnFlip;
        public Action OnDismatch;
        public Action OnWin;
        public Action<Dictionary<Vector3Int, int>, Vector2Int> OnSave;
    }
}