using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierGun : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;

    private GameObject player = null;

    private float timeBtwShots;
    public float startTimeBtwShots;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 difference = player.transform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            if (timeBtwShots <= 0)
            {
                Instantiate(bullet, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + offset));
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
