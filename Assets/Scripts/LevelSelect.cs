using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int sceneToLoad;


    public void OnButtonPress()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
