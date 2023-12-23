using System;
using UnityEngine;
using static System.Math;

public class BearBar : MonoBehaviour
{
    public ProgressBar pb;
    public BossBearEnemy bear;

    void Update()
    {
        pb.BarValue = (float)Round(Convert.ToDouble(bear.health), 2);
    }
}
