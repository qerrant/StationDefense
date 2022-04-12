using StationDefense.Game;
using UnityEngine;

namespace StationDefense.Components
{
    public class Station : Damageable
    {
        public float maxHealth = 0;
        public float damage = 0;
        public GameObject stationModel;

        private void Start()
        {
            SetMaxHealth(maxHealth);
            SetDamage(damage);

            Tile ground = GetComponentInParent<Tile>();
            if (ground != null)
            {
                ground.Used = true;
            }
        }

        protected override void OnDead()
        {
            base.OnDead();
            stationModel.SetActive(false);
            if (WaveSpawner.instanceExists)
            {
                WaveSpawner.instance.StopWaves();
            }
            Invoke("ShowGameOver", 1.0f);
            AudioSource sfx = GetComponent<AudioSource>();
            if (sfx == null) return;
            sfx.Play();
        }

        private void ShowGameOver()
        {
            if (!GameManager.instanceExists) return;
            GameManager.instance.UpdateGameState(GameState.Lose);
        }
    }
}
