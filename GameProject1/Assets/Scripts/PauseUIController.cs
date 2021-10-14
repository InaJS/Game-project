using UnityEngine;


public class PauseUIController : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    public void SwitchObjectActive()
    {
        this.gameObject.SetActive(!gameObject.activeInHierarchy);

        if (gameObject.activeInHierarchy)
        {
            music.Pause();
            Time.timeScale = 0;
        }
        else
        {
            music.UnPause();
            Time.timeScale = 1.0f;
        }
    }
}