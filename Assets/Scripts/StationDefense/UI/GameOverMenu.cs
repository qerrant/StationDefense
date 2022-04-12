using StationDefense.Game;
using UnityEngine;

namespace StationDefense.UI
{
    public class GameOverMenu : MonoBehaviour
    {
        public SimplePage gameOverMenu;
        public Canvas HUD;

        private void Awake()
        {
            GameManager.OnGameStateChange += OnStateChanged;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnStateChanged;
        }

        private void OnStateChanged(GameState state)
        {
            if (state == GameState.Lose)
            {
                gameOverMenu.Show();
                HUD.enabled = false;
                gameOverMenu.GetComponentInChildren<Animation>()?.Play();
            }
        }
    }
}
