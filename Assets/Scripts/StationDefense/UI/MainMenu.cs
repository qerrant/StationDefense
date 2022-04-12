using StationDefense.Game;
using UnityEngine;

namespace StationDefense.UI
{
    public class MainMenu : MonoBehaviour
    {
        public SimplePage mainMenuPage;
        public SimplePage settingsMenuPage;
        public LevelMenu levelMenuPage;
        public LoadingScreen loadingScreen;
        private SimplePage _currentPage;

        private void Start()
        {
            ShowMainMenu();
        }

        public void BackWithSave()
        {
            ShowMainMenu();
            if (!GameManager.instanceExists) return;
            GameManager.instance.SaveData();
        }

        public void ShowMainMenu()
        {
            ActivatePage(mainMenuPage);
        }

        public void ShowSettingsMenu()
        {
            ActivatePage(settingsMenuPage);
        }

        public void ShowLevelMenu()
        {
            ActivatePage(levelMenuPage);
        }

        public void ShowLoadingScreen(string sceneName)
        {
            ActivatePage(loadingScreen);
            loadingScreen.StartScene(sceneName);
        }

        public void ActivatePage(SimplePage page)
        {
            DeactiovatePage();
            _currentPage = page;
            page.Show();
        }

        public void DeactiovatePage()
        {
            if (_currentPage != null)
            {
                _currentPage.Hide();
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
