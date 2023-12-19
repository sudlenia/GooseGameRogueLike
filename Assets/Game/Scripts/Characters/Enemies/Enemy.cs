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


    [SerializeField]
    [Tooltip("Prefab feather")]
    private GameObject featherPrefab;
    [SerializeField]
    [Tooltip("Count feather drop")]
    public int featherDropAmount = 1;


    public override void Die()
    {
        for (int i = 0; i < featherDropAmount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            Instantiate(featherPrefab, transform.position + randomOffset, Quaternion.identity);
        }
        Destroy(gameObject);

    }

    

    public void GetDamage(float amount)
    {
        health -= amount;
        
        if (health <= 0) Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainGoose"))
        {
            MainGoose goose = other.GetComponent<MainGoose>();
            if (goose != null)
            {
                goose.GetDamage(damage + (damage*damageIncrease));
            }
        }
    }
}