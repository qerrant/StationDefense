using UnityEngine;

namespace StationDefense.Components
{
    public class Damageable : MonoBehaviour
    {
        public ParticleSystem boomVFX;
        private Health _healthComponent;
        private Damager _damagerComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<Health>();

            if (_healthComponent != null)
            {
                _healthComponent.deadAction += OnDead;
            }

            _damagerComponent = GetComponent<Damager>();
        }

        protected float GetHealth() => _healthComponent.GetHealth();

        protected void SetMaxHealth(float health)
        {
            _healthComponent?.SetMaxHealth(health);
        }

        protected void SetDamage(float damage)
        {
            _damagerComponent?.SetDamage(damage);
        }

        protected virtual void OnDead()
        {
            boomVFX?.Play();
        }
    }
}
