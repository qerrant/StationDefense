using UnityEngine;

namespace StationDefense.Level
{
    [CreateAssetMenu(fileName = "LevelData.asset", menuName = "StationDefense/Level Configuration", order = 1)]
    public class LevelData : ScriptableObject
    {
        public string levelName;
        public string description;
        public string sceneName;        
    }
}