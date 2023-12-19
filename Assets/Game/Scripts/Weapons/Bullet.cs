using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public float damage;
    public LayerMask whatIsSolid;

    //public MainGoose goose;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", lifetime);
        //speed += goose.baseAireRate;
        //damage += goose.baseDamage;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                //hitInfo.collider.GetComponent<Enemy>().Die();
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
