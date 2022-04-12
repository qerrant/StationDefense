using UnityEngine;

namespace StationDefense.Building
{
	public class BuildingLevel : MonoBehaviour
	{
		public BuildingData buildingData;

		public string fullname
		{
			get { return buildingData.fullName; }
		}
		public string description
		{
			get { return buildingData.description; }
		}
		public float maxHealth
		{
			get { return buildingData.maxHealth; }
		}
		public float visibleRadius
		{
			get { return buildingData.visibleRadius; }
		}
		public float damage
		{
			get { return buildingData.damage; }
		}

		public float obstacleDamage
		{
			get { return buildingData.obstacleDamage; }
		}

		public int cost
		{
			get { return buildingData.cost; }
		}
		public int sell
		{
			get { return buildingData.sell; }
		}

		public Sprite icon
		{
			get { return buildingData.icon; }
		}
	}
}
