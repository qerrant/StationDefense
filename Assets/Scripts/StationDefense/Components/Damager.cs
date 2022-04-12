using UnityEngine;

namespace StationDefense.Components
{
    public class Damager : MonoBehaviour
    {
        private float damage;

        public float GetDamage() => damage;
        public void SetDamage(float damage)
        {
            this.damage = damage;
        }
    }
}
