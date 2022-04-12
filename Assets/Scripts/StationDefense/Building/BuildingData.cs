using UnityEngine;

namespace StationDefense.Building
{
    [CreateAssetMenu(fileName = "BuildingData.asset", menuName = "StationDefense/Building Configuration", order = 1)]
    public class BuildingData : ScriptableObject
    {
        public string fullName;
        public string description;
        public float maxHealth;
        public float visibleRadius;
        public float damage;
        public float obstacleDamage;
        public int cost;
        public int sell;
        public Sprite icon;
    }
}
