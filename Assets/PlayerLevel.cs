using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public Text level;
    void FixedUpdate()
    {
        level.text = DataHolder.stats[1].ToString() + " уровень";
    }
}
