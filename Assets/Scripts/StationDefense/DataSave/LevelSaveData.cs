using System;

namespace StationDefense.DataSave
{
	[Serializable]
	public class LevelSaveData
	{
		public string levelName;
		public int waves;

		public LevelSaveData(string levelId, int numberOfWaves)
		{
			levelName = levelId;
			waves = numberOfWaves;
		}
	}
}