using StationDefense.DataSave;
using StationDefense.Level;
using StationDefense.Patterns;
using StationDefense.Sound;
using System;
using UnityEngine;
using UnityEngine.Audio;


namespace StationDefense.Game
{
    public class GameManager : Singleton<GameManager>
    {
        public LevelData[] levels;
        public AudioMixer gameMixer;
        public static event Action<GameState> OnGameStateChange;
        private float _startTimeScale;
        private JsonSave<GameDataStore> _dataSaver;
        private GameDataStore _data;
        public GameState State { get; set; }
        public string CurrentLevel { get; set; }
        public int CurrentWave { get; set; }
        public SoundData SoundData { get; set; }


        protected override void Awake()
        {
            base.Awake();
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            _startTimeScale = Time.timeScale;

            _dataSaver = new JsonSave<GameDataStore>("gamesave"); //_dataSaver.Delete();
            LoadData();

            UpdateGameState(GameState.MainMenu);

            DontDestroyOnLoad(this);
        }

        protected static float LogarithmicDbTransform(float volume)
        {
            volume = (Mathf.Log(89 * volume + 1) / Mathf.Log(90)) * 80;
            return volume - 80;
        }

        public void SetMasterVolume(float volume)
        {
            _data.masterVolume = volume;
            gameMixer.SetFloat("Master", LogarithmicDbTransform(Mathf.Clamp01(volume)));
        }

        public void SetSoundVolume(float volume)
        {
            _data.sfxVolume = volume;
            gameMixer.SetFloat("SFX", LogarithmicDbTransform(Mathf.Clamp01(volume)));
        }

        public void SetMusicVolume(float volume)
        {
            _data.musicVolume = volume;
            gameMixer.SetFloat("Music", LogarithmicDbTransform(Mathf.Clamp01(volume)));
        }

        public void SaveData()
        {
            _dataSaver.Save(_data);
        }

        public void LoadData()
        {
            if (!_dataSaver.Load(out _data))
            {
                _data = new GameDataStore
                {
                    sfxVolume = 1.0f,
                    musicVolume = 1.0f,
                    masterVolume = 1.0f
                };
            }

            gameMixer.SetFloat("Master", LogarithmicDbTransform(Mathf.Clamp01(_data.masterVolume)));
            gameMixer.SetFloat("SFX", LogarithmicDbTransform(Mathf.Clamp01(_data.sfxVolume)));
            gameMixer.SetFloat("Music", LogarithmicDbTransform(Mathf.Clamp01(_data.musicVolume)));

            SoundData = new SoundData();
            SoundData.sfxVolume = _data.sfxVolume;
            SoundData.musicVolume = _data.musicVolume;
            SoundData.masterVolume = _data.masterVolume;
        }

        public int GetWavesForLevel(string levelName)
        {
            return _data.GetWavesForLevel(levelName);
        }

        public void UpdateGameState(GameState state)
        {
            this.State = state;

            switch (state)
            {
                case GameState.Game:
                    break;
                case GameState.OpenPauseMenu:
                    UnfreezeTime();
                    break;
                case GameState.ClosePauseMenu:
                    UnfreezeTime();
                    break;
                case GameState.Pause:
                    FreezeTime();
                    break;
                case GameState.Lose:
                    _data.CompleteLevel(CurrentLevel, CurrentWave);
                    SaveData();
                    break;
                case GameState.MainMenu:
                    UnfreezeTime();
                    break;
                case GameState.ShowRound:
                    break;
                case GameState.Settings:
                    UnfreezeTime();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            OnGameStateChange?.Invoke(state);
        }

        public void FreezeTime()
        {
            _startTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public void UnfreezeTime()
        {
            Time.timeScale = _startTimeScale;
        }
    }
}
