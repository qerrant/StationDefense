using StationDefense.Level;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace StationDefense.UI
{
    public class LevelButton : MonoBehaviour
    {
        public Text levelName;
        public Text description;
        public Text wavesResult;
        public event Action<string, string> OnLevelClick;
        private string _scene;

        public void SetInfo(LevelData data, int result)
        {
            levelName.text = data.levelName;
            description.text = data.description;
            wavesResult.text = $"{result} waves";
            _scene = data.sceneName;
        }

        public void OnClick()
        {
            OnLevelClick?.Invoke(_scene, levelName.text);
        }
    }
}
