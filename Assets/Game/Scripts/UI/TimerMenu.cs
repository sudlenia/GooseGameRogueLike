using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerMenu : MonoBehaviour
{
    public float TimeStart = 6.0f;
    public Text Timer;
    public GameObject TimerPanel;
    void Start()
    {
        Timer.text = TimeStart.ToString();
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        if (TimeStart >= 0)
        {
            TimeStart -= Time.deltaTime;
            Timer.text = Mathf.Round(TimeStart).ToString();
        }
        else
        {
            TimerPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
