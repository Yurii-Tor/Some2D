using System;
using System.Collections.Generic;
using MechingCards.Common;
using UnityEngine;

namespace MechingCards.SaveSystemService {
    [Serializable]
    public class SaveModel : ISaveModel {
        [SerializeField] string m_gameData;
        [SerializeField] private Vector2Int m_size;
        public SaveModel(Dictionary<Vector3Int, int> data, Vector2Int size) {
            var serializableDict = new CardsDictionaryWrapper(data);
            m_gameData = JsonUtility.ToJson(serializableDict);
            m_size = size;
        }

        public Dictionary<Vector3Int, int> GetGameData() {
            CardsDictionaryWrapper wrapper =  JsonUtility.FromJson<CardsDictionaryWrapper>(m_gameData);
            return wrapper.ToDictionary();
        }

        public Vector2Int GetSize() {
            return m_size;
        }
    }
}