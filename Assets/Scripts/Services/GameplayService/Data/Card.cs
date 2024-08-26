namespace MechingCards.GameplayService {
	public class Card {
		private int m_ID;
		private bool m_isLocked = false;
		public int ID => m_ID;

		public bool IsLocked => m_isLocked;
		
		public Card(int id) {
			m_ID = id;
		}

		public void Lock() {
			m_isLocked = true;
		}

		public void Unlock() {
			m_isLocked = false;
		}
	}
}