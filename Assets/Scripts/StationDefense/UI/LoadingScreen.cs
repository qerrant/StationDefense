using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StationDefense.UI
{    
    public class LoadingScreen : SimplePage
    {
        public HealthBar loadingBar;
        public Text loadingText;
        private string _sceneName;

        void Start()
        {
            loadingBar.SetMaxHealth(100);            
            loadingBar.SetHealth(0);
        }

        public void StartScene(string sceneName)
        {
            _sceneName = sceneName;
            StartCoroutine(LoadScene());
        }


        private IEnumerator LoadScene()
        {
            yield return null;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneName);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                loadingBar.SetHealth(asyncOperation.progress * 100);

                if (asyncOperation.progress >= 0.9f)
                {

                    loadingText.text = "Tap to continue";

                    if (Input.GetMouseButtonDown(0))
                        asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
