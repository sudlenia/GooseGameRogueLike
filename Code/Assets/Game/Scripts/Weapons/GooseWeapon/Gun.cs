using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private AudioSource shoot;

    public float offset;
    public GameObject bullet;
    public Transform shotPoint;

    private GameObject closestEnemy = null;

    private float timeBtwShots;
    public float startTimeBtwShots;
    
    void Update()
    {
        closestEnemy = findClosestEnemy();

        if (closestEnemy != null)
        {
            Vector3 difference = closestEnemy.transform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            if (timeBtwShots <= 0)
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                shoot.Play();
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    GameObject findClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        GameObject closestEnemy = null;

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach(GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closestEnemy = enemy;
                distance = curDistance;
            }
        }
        return closestEnemy;
    }
}
