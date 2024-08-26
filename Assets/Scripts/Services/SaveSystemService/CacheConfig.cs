using System.IO;
using UnityEngine;

namespace MechingCards.SaveSystemService {
	public static class CacheConfig {
		public static string CachePath => Path.Combine(Application.persistentDataPath, "ApplicationCache");
		public static string PersistentDataFolder => Path.Combine(CachePath, "PersistentData");
		public static string PersistentDataPath => Path.Combine(PersistentDataFolder, "data");
	}
}