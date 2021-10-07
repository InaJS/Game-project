using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/SceneTransition")]
public class SceneTransition : ScriptableObject
{
    public string sceneName;

    public void ChangeToScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
