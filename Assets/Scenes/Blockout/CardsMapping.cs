using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsMapping", menuName = "MatchingCards/Cards Mapping", order = 1)]
public class CardsMapping : ScriptableObject {
	[SerializeField] private List<Sprite> m_spritesList;
	public List<Sprite> SpritesList => m_spritesList;
}
