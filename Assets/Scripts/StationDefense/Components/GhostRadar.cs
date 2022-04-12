using StationDefense.Building;
using StationDefense.Enemy;
using UnityEngine;

namespace StationDefense.Components
{
    public class GhostRadar : MonoBehaviour
    {
        public LayerMask targetLayer;
        public float Radius;

        private void Start()
        {
            BuildingLevel level = GetComponent<BuildingLevel>();
            if (level != null)
            {
                Radius = level.visibleRadius;
            }
        }

        private void FixedUpdate()
        {
            DetectGhost();
        }

        private void DetectGhost()
        {
            GameObject[] gObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            foreach (GameObject gObject in gObjects)
            {
                if ((targetLayer.value & (1 << gObject.layer)) > 0)
                {
                    EnemyAgent enemy = gObject.GetComponent<EnemyAgent>();
                    if (enemy == null) continue;
                    if (!enemy.Ghost) continue;
                    var curDist = Vector3.Distance(enemy.transform.position, this.transform.position);
                    if (curDist <= Radius)
                    {
                        enemy.UnGhost();
                    }
                }
            }
        }
    }
}
