using UnityEngine;
using UnityEngine.UI;

namespace StationDefense.UI
{
    public class ProgressBar : MonoBehaviour
    {
        public Slider slider;
        public Image fill;
        public Gradient gradient;

        public void SetProgress(float amount)
        {
            slider.value = amount;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        public void SetMaxProgress(float amount)
        {
            slider.maxValue = amount;
            slider.value = amount;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        public void SetVisible(bool isVisible)
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
            {
                canvas.enabled = isVisible;
            }
        }
    }
}
