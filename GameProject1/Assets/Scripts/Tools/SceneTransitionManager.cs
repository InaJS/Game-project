using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Image blackScreen;
    [SerializeField] private SongWaves currentSong;
    [SerializeField] private SongWaves selectedSong;
    [SerializeField] private float fadeTime = 1.0f;

    public void FadeToSceneTransition()
    {
        StartCoroutine(FadeAnimation());
    }

    private IEnumerator FadeAnimation()
    {
        float elapsedTime = 0.0f;
        Color c = blackScreen.color;

        blackScreen.raycastTarget = true;

        if (currentSong != null)
        {
            currentSong.SongAudio = selectedSong.SongAudio;
            currentSong.SongBpm.bpm = selectedSong.SongBpm.bpm;
            currentSong.EnemyWaves = selectedSong.EnemyWaves;
        }
        
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime ;
            c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            blackScreen.color = c;
        }
        
        SceneManager.LoadScene(sceneName);
    }
    
}
