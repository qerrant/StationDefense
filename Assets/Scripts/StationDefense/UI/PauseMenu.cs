using StationDefense.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StationDefense.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public SimplePage pauseMenu;
        public SimplePage settingsPage;
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
            switch (state)
            {
                case GameState.OpenPauseMenu:
                    pauseMenu.Show();
                    HUD.enabled = false;
                    break;
                case GameState.ClosePauseMenu:
                    pauseMenu.Hide();
                    HUD.enabled = true;
                    break;
                case GameState.Settings:
                    pauseMenu.Hide();
                    settingsPage.Show();
                    break;
                case GameState.MainMenu:
                    SceneManager.LoadScene("MainMenu");
                    break;
            }
        }

        public void OpenPauseMenu()
        {
            GameManager.instance.UpdateGameState(GameState.OpenPauseMenu);
        }

        public void ClosePauseMenu()
        {
            GameManager.instance.UpdateGameState(GameState.ClosePauseMenu);
        }

        public void PauseGame()
        {
            GameManager.instance.UpdateGameState(GameState.Pause);
        }

        public void ResumeGame()
        {
            GameManager.instance.UpdateGameState(GameState.Game);
        }

        public void ShowSettings()
        {
            GameManager.instance.UpdateGameState(GameState.Settings);
        }

        public void CloseSettings()
        {
            settingsPage.Hide();
            OpenPauseMenu();
        }

        public void QuitToMainMenu()
        {
            GameManager.instance.UpdateGameState(GameState.MainMenu);
        }
    }
}
