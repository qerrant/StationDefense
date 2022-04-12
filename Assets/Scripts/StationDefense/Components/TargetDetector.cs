using StationDefense.Building;
using StationDefense.Enemy;
using StationDefense.Game;
using UnityEngine;

namespace StationDefense.Components
{
    public class TargetDetector : MonoBehaviour
    {
        public Transform rotator;
        public Transform gun;
        private EnemyAgent _targetEnemy;
        private Attacker _attacker;
        private float _radius;

        private void Start()
        {
            BuildingLevel level = GetComponent<BuildingLevel>();
            _radius = level != null ? level.visibleRadius : 2.0f;
            _attacker = GetComponent<Attacker>();
        }

        private void Update()
        {
            FindTarget();

            if (_targetEnemy != null)
            {
                LookAtTarget();
                _attacker?.StartShoot();
            }
            else
            {
                _attacker?.StopShoot();
            }
        }

        private void FindTarget()
        {
            _targetEnemy = null;
            if (!WaveSpawner.instanceExists) return;
            EnemyAgent enemy = WaveSpawner.instance.GetNearestEnemy(this.transform.position, _radius);            
            _targetEnemy = enemy != null && !enemy.Ghost ? enemy : null;            
        }

        private void LookAtTarget()
        {
            var oldRotator = rotator.rotation;
            rotator.LookAt(_targetEnemy.transform);
            Quaternion target = Quaternion.Euler(oldRotator.eulerAngles.x, rotator.rotation.eulerAngles.y, oldRotator.eulerAngles.z);
            rotator.rotation = target;

            var oldGun = gun.rotation;
            gun.LookAt(_targetEnemy.transform);
            target = Quaternion.Euler(gun.rotation.eulerAngles.x, oldGun.eulerAngles.y, oldGun.eulerAngles.z);
            gun.rotation = target;
        }
    }
}
