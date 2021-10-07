using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tools
{
    public class FadeTextBehavior : MonoBehaviour
    {
        [SerializeField] private Text fadeText;
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
            fadeText.color = startColor;
            
            fadeText.gameObject.SetActive(true);

            while (elapsedTime < fadeTime)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                Color newColor = Color.Lerp(startColor, targetColour, Mathf.Clamp01(elapsedTime / fadeTime));
                fadeText.color = newColor;
            }
            
            fadeText.gameObject.SetActive(disableOnFinish);
        }
    }
}