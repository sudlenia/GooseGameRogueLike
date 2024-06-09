using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEnemy : Enemy
{
    //public SoldierEnemy()
    //{
    //    health = 75;
    //    damage = 5;
    //    shootDamage = 10;
    //    speed = 0.8f;
    //    featherDropAmount = 2;
    //}
    public float distance;
    public override void Move()
    {
        if(Vector2.Distance(transform.position, goose.transform.position) >= distance)
        {
            base.Move();
        }
    }
}
