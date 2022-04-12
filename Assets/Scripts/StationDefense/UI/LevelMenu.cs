using StationDefense.Game;
using StationDefense.Level;
using UnityEngine;
using UnityEngine.UI;

namespace StationDefense.UI
{
    public class LevelMenu : SimplePage
    {
        public GameObject levelButton;
        public LayoutGroup layout;
        public MainMenu mainMenu;

        private void Start()
        {
            if (GameManager.instanceExists)
            {
                foreach (LevelData level in GameManager.instance.levels)
                {
                    AddButton(level);
                }
            }
        }
        private void AddButton(LevelData data)
        {
            GameObject btn = Instantiate(levelButton);
            LevelButton levelBtn = btn.GetComponent<LevelButton>();
            int waves = GameManager.instance.GetWavesForLevel(data.levelName);
            levelBtn.SetInfo(data, waves);
            levelBtn.OnLevelClick += LoadScene;
            btn.transform.SetParent(layout.transform);
            btn.transform.localScale = Vector3.one;
        }

        private void LoadScene(string sceneName, string levelName)
        {
            mainMenu.ShowLoadingScreen(sceneName);
            if (GameManager.instanceExists)
            {
                GameManager.instance.UpdateGameState(GameState.Game);
                GameManager.instance.CurrentLevel = levelName;
            }
        }
    }
}
