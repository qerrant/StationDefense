using StationDefense.Game;
using UnityEngine;
using UnityEngine.UI;

namespace StationDefense.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public Slider masterSlider;
        public Slider sfxSlider;
        public Slider musicSlider;

        private void Start()
        {
            if (GameManager.instanceExists)
            {
                InitSliders(GameManager.instance.SoundData);
            }
        }

        public void InitSliders(SoundData data)
        {
            masterSlider.value = data.masterVolume;
            sfxSlider.value = data.sfxVolume;
            musicSlider.value = data.musicVolume;
        }


        public static void SetSoundVolume(float volume)
        {
            if (!GameManager.instanceExists) return;
            GameManager.instance.SetSoundVolume(volume);
        }

        public static void SetMusicVolume(float volume)
        {
            if (!GameManager.instanceExists) return;
            GameManager.instance.SetMusicVolume(volume);
        }

        public static void SetMasterVolume(float volume)
        {
            if (!GameManager.instanceExists) return;
            GameManager.instance.SetMasterVolume(volume);
        }
    }
}
