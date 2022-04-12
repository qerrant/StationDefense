using StationDefense.UI;
using UnityEngine;
using UnityEngine.Events;

namespace StationDefense.Components
{
    public class Health : MonoBehaviour
    {
        public HealthBar healthBar;
        public UnityAction deadAction;
        private float _health;
        private float _maxHealth = 0;

        public float GetHealth() => _health;

        public void SetMaxHealth(float health)
        {
            _maxHealth = health;
            healthBar?.SetMaxHealth(_maxHealth);
            _health = health;
        }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
            healthBar?.SetHealth(_health);
            if (_health <= 0)
            {
                healthBar?.SetVisible(false);
                deadAction?.Invoke();
            }
        }
    }
}
