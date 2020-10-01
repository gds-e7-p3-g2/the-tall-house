using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameDelayed(float delay = 1f)
    {
        Invoke("LoadGame", delay);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("prototype");
    }

    public void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
