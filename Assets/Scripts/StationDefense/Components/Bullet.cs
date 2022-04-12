using UnityEngine;

namespace StationDefense.Components
{
    public class Bullet : MonoBehaviour
    {
        public float lifeTime = 3.0f;

        void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
