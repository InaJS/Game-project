using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tools
{
    public class FadeBehavior : MonoBehaviour
    {
        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeTime = 1.0f;
        [SerializeField] private Color targetColour = Color.white;
        private Color startColor;

        public void RequestFade()
        {
            StartCoroutine(FadeAnimation());
        }

        private IEnumerator FadeAnimation()
        {
            float elapsedTime = 0.0f;
            startColor = fadeImage.color;

            while (elapsedTime < fadeTime)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                Color newColor = Color.Lerp(startColor, targetColour, Mathf.Clamp01(elapsedTime / fadeTime));
                fadeImage.color = newColor;
            }
        }
    }
}