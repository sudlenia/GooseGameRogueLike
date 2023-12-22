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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1.0f;
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
}
