using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public float damage;
    public float damageIncrease;
    public LayerMask whatIsSolid;

    void Start()
    {
        Invoke("DestroyBullet", lifetime);
        damageIncrease = DataHolder.damageIncrease;
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<MainGoose>().GetDamage(damage + damage * damageIncrease);
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
