using System;
using UnityEngine;
using static System.Math;

public class HealthBar : MonoBehaviour
{
    public ProgressBar pb;
    public MainGoose goose;

    void Update()
    {
        pb.BarValue = (float)Round(Convert.ToDouble(goose.health), 2);
    }
}
