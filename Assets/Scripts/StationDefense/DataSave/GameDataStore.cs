using System.Collections.Generic;
using UnityEngine;

namespace StationDefense.DataSave
{
	public sealed class GameDataStore : IDataStore
	{
		public float masterVolume = 1.0f;

		public float sfxVolume = 1.0f;

		public float musicVolume = 1.0f;

		public List<LevelSaveData> levels = new List<LevelSaveData>();

		public void CompleteLevel(string levelName, int wavesEarned)
		{
			foreach (LevelSaveData level in levels)
			{
				if (level.levelName == levelName)
				{
					level.waves = Mathf.Max(level.waves, wavesEarned);
					return;
				}
			}
			levels.Add(new LevelSaveData(levelName, wavesEarned));
		}

		public int GetWavesForLevel(string levelName)
		{
			foreach (LevelSaveData level in levels)
			{
				if (level.levelName == levelName)
				{
					return level.waves;
				}
			}
			return 0;
		}
	}
}