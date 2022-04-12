using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace StationDefense.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SimplePage : MonoBehaviour, ISimplePage
    {
        public Canvas canvas;
        public float time;
        public UnityEvent OnRunBeforeOpen;
        public UnityEvent OnRunAfterOpen;
        public UnityEvent OnRunBeforeClose;
        public UnityEvent OnRunAfterClose;

        public virtual void Show()
        {
            StartCoroutine(FadeIn());
        }

        public virtual void Hide()
        {
            StartCoroutine(FadeOut());
        }


        private IEnumerator FadeIn()
        {
            canvas.enabled = true;
            OnRunBeforeOpen?.Invoke();
            for (float a = 0; a <= time; a += Time.deltaTime)
            {
                float alpha = Mathf.Clamp01(a / time);
                canvas.GetComponent<CanvasGroup>().alpha = alpha;
                yield return null;
            }
            OnRunAfterOpen?.Invoke();
        }

        private IEnumerator FadeOut()
        {
            OnRunBeforeClose?.Invoke();
            for (float a = time; a >= 0; a -= Time.deltaTime)
            {
                float alpha = Mathf.Clamp01(a / time);
                canvas.GetComponent<CanvasGroup>().alpha = alpha;
                yield return null;
            }
            OnRunAfterClose?.Invoke();
            canvas.enabled = false;
        }
    }
}
