using System.Collections;
using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField]
    [Tooltip("Health")]
    public float health;

    [SerializeField]
    [Tooltip("Damage")]
    public float damage;

    [SerializeField]
    [Tooltip("Speed")]
    public float speed;

    [SerializeField]
    [Tooltip("Damage boost")]
    public float damageIncrease = 0.1f;

    public Transform goose;
    private bool playerInside = false;

    [SerializeField]
    [Tooltip("Prefab feather")]
    private GameObject featherPrefab;
    [SerializeField]
    [Tooltip("Count feather drop")]
    public int featherDropAmount = 1;

    private Animator animX;
    private Animator animY;
    private Rigidbody2D rb;
    private bool facingLeft = true;
    private SpriteRenderer sr;

    private void Start()
    {
        animX = GetComponent<Animator>();
        animY = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public override void Die()
    {
        for (int i = 0; i < featherDropAmount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
            Instantiate(featherPrefab, transform.position + randomOffset, Quaternion.identity);
        }
        Destroy(gameObject);
    }



    public void GetDamage(float amount)
    {
        health -= amount;

        if (health <= 0) Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            StartCoroutine(DelayedDamage(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
    private System.Collections.IEnumerator DelayedDamage(Collider2D collision)
    {
        while (true)
        {
            if (playerInside)
            {
                MainGoose goose = collision.GetComponent<MainGoose>();
                if (goose != null)
                {
                    goose.GetDamage(damage + (damage * damageIncrease));
                }
            }
            else { break; }
            yield return new WaitForSeconds(1);
        }
    }

    private void Awake()
    {
        goose = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        damageIncrease = DataHolder.damageIncrease;
    }

    private void Update()
    {
        animX.SetFloat("moveX", Mathf.Abs(rb.position.x));
        animY.SetFloat("moveY", Mathf.Abs(rb.position.y));
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        Vector2 direction = (goose.position - transform.position).normalized;
        rb.position = Vector2.MoveTowards(transform.position, goose.position, speed * Time.deltaTime);

        if (!facingLeft && direction.x < 0)
        {
            Flip();
        }
        else if (facingLeft && direction.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        sr.flipX = !sr.flipX;
    }
}