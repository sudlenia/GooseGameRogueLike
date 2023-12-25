using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public AudioSource uiAudio;
    public AudioSource gameAudio;

    public GameObject PauseMenu;
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 == 4) DataHolder.stats[0] = 100;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1.0f;
        DataHolder.damageIncrease += 0.1f;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        uiAudio.Pause();
        gameAudio.Play();
    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        uiAudio.Play();
        gameAudio.Pause();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
