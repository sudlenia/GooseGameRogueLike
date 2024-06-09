using System;
using UnityEngine;
using static System.Math;

public class BearBar : MonoBehaviour
{
    public ProgressBar pb;
    public BossBearEnemy bear;
    public TimerMenu timer;

    void Update()
    {
        pb.BarValue = (float)Round(Convert.ToDouble(bear.health / 1000 * 100), 2);
        if (bear.health <= 0)
            timer.EndOfScene();
    }
}
