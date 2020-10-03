using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadTutDelayed(float delay = 1f)
    {
        Invoke("loadTut", delay);
    }

    public void loadTut()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("tutorial");
    }

    public void LoadGameDelayed(float delay = 1f)
    {
        Invoke("LoadGame", delay);
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("prototype");
    }

    public void LoadMenuDelayed(float delay = 1f)
    {
        Invoke("LoadMenu", delay);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevel(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
