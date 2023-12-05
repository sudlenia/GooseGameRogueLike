using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEnemy : Enemy
{
    [SerializeField]
    [Tooltip("Урон от выстрела")]
    public float shootDamage;
    //public SoldierEnemy()
    //{
    //    health = 75;
    //    damage = 5;
    //    shootDamage = 10;
    //    speed = 0.8f;
    //    featherDropAmount = 2;
    //}

    public void Fire()
    {
        //Стрельба солдата
    }
}
