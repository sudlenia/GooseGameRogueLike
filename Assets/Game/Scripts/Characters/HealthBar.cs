using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public ProgressBar pb;
    public MainGoose goose;

    void Update()
    {
        pb.BarValue = Mathf.Round(goose.health);
    }
}
