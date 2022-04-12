using StationDefense.Game;
using UnityEngine;
using UnityEngine.UI;

namespace StationDefense.UI
{
    public class WaveRound : SimplePage
    {
        public Text waveRound;
        public float closeTime = 2.0f;

        public void ShowRound(int round)
        {
            if (GameManager.instanceExists)
            {
                GameManager.instance.UpdateGameState(GameState.ShowRound);
            }
            waveRound.text = $"WAVE {round}";
            Show();
            canvas.GetComponentInChildren<Animation>()?.Play();
            Invoke("Close", closeTime);
        }

        private void Close()
        {
            Hide();
            if (GameManager.instanceExists)
            {
                GameManager.instance.UpdateGameState(GameState.Game);
            }
        }
    }
}
