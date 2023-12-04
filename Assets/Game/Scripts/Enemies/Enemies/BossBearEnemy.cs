using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearEnemy : Enemy
{
    public BossBearEnemy()
    {
        health = 2500;
        damage = 50;
        speed = 0.75f;
        featherDropAmount = 0;
    }
}
