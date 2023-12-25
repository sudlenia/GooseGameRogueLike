using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerMenu : MonoBehaviour
{
    public float TimeStart = 6.0f;
    public Text Timer;
    public GameObject TimerPanel;
    public Button AudioManager;
    public BossBearEnemy bear;
    void Start()
    {
        Timer.text = "Уровень";
        if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex < 3)
            Timer.text += " 1   ";
        else Timer.text += " 2   ";
        Timer.text += TimeStart.ToString();
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        if (TimeStart >= 0)
        {
            Timer.text = "Уровень";
            if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex < 3)
                Timer.text += " 1   ";
            else Timer.text += " 2   ";
            TimeStart -= Time.deltaTime;
            Timer.text += Mathf.Round(TimeStart).ToString();
        }
        else EndOfScene();
    }
    public void EndOfScene()
    {
        TimerPanel.SetActive(true);
        Time.timeScale = 0f;
        if (!AudioManager.uiAudio.isPlaying) AudioManager.uiAudio.Play();
        if (AudioManager.gameAudio.isPlaying) AudioManager.gameAudio.Pause();
    }
}
