using StationDefense.Building;
using StationDefense.Game;
using System.Collections;
using UnityEngine;

namespace StationDefense.Components
{
    public class Attacker : MonoBehaviour
    {
        [Tooltip("Shoots per second")]
        public float bulletSpeed = 5.0f;
        public float shootRate = 1.0f;
        public Transform bulletSocket;
        public GameObject bulletPrefab;
        private Coroutine _coroutine;
        private bool _isActive = false;


        public void StartShoot()
        {
            if (_isActive) return;
            _isActive = true;
            _coroutine = StartCoroutine("Shoot");
        }

        public void StopShoot()
        {
            if (_coroutine == null) return;
            StopCoroutine(_coroutine);
            _isActive = false;
        }

        private IEnumerator Shoot()
        {
            while (true)
            {
                if (GameManager.instance.State != GameState.Game)
                {
                    StopShoot();
                    yield return null;
                }
                GameObject bullet = Instantiate(bulletPrefab, bulletSocket.position, bulletSocket.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSocket.forward * bulletSpeed;
                BuildingLevel level = GetComponent<BuildingLevel>();
                bullet.GetComponent<Damager>().SetDamage(level.damage);
                yield return new WaitForSeconds(1 / shootRate);
            }
        }
    }
}
