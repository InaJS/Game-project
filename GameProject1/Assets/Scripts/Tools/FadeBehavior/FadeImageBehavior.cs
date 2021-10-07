using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tools
{
    public class FadeImageBehavior : MonoBehaviour
    {
        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeTime = 1.0f;
        [SerializeField] private Color startColor;
        [SerializeField] private Color targetColour = Color.white;
        [SerializeField] private bool disableOnFinish = true;

        public void RequestFade()
        {
            StartCoroutine(FadeAnimation());
        }

        private IEnumerator FadeAnimation()
        {
            float elapsedTime = 0.0f;
            fadeImage.color = startColor;
            
            fadeImage.gameObject.SetActive(true);

            while (elapsedTime < fadeTime)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                Color newColor = Color.Lerp(startColor, targetColour, Mathf.Clamp01(elapsedTime / fadeTime));
                fadeImage.color = newColor;
            }
            
            fadeImage.gameObject.SetActive(disableOnFinish);
        }
    }
}