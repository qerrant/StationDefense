using StationDefense.Components;
using UnityEngine;

namespace StationDefense.Building
{
    public class Building : Damageable
    {
        public BuildingLevel[] buildings;
        private int level = 0;
        private BuildingLevel _currentBuilding;
        private Tile _ground;

        private void Start()
        {
            _currentBuilding = Instantiate(buildings[level], transform);
            SetMaxHealth(_currentBuilding.maxHealth);
            SetDamage(_currentBuilding.obstacleDamage);
         
        }

        protected override void OnDead()
        {
            base.OnDead();
            Dead();
        }

        public void SetGround(Tile tile)
        {
            _ground = tile;
        }

        public float GetMaxHealth()
        {
            return _currentBuilding.maxHealth;
        }

        public void Upgrade()
        {
            if (level < buildings.Length)
            {
                level++;

                if (_currentBuilding != null)
                {
                    Destroy(_currentBuilding.gameObject);
                }

                _currentBuilding = Instantiate(buildings[level], transform);

                SetMaxHealth(_currentBuilding.maxHealth);
                SetDamage(_currentBuilding.obstacleDamage);
            
            }
        }

        public bool CanUpgrade()
        {
            return level + 1 < buildings.Length;
        }

        public BuildingLevel GetUpgradeInfo()
        {
            return buildings[Mathf.Clamp(level + 1, 0, buildings.Length - 1)];
        }

        private void Dead()
        {
            _ground.Used = false;
            _currentBuilding.gameObject.SetActive(false);
            Destroy(gameObject, 1.0f);
            AudioSource sfx = GetComponent<AudioSource>();
            if (sfx == null) return;
            sfx.Play();
        }       
       
    }
}
