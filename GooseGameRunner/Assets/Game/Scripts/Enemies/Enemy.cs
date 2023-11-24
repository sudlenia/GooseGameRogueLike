using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Здоровье")]
    public float health;

    [SerializeField]
    [Tooltip("Урон")]
    public float damage;

    [SerializeField]
    [Tooltip("Скорость передвежения")]
    public float speed;

    [SerializeField]
    [Tooltip("Увеличение урона с течением волны")]
    public float damageIncrease = 0.1f;


    [SerializeField]
    [Tooltip("Префаб пера")]
    private GameObject featherPrefab;
    [SerializeField]
    [Tooltip("Количество выпадающих перьев")]
    public int featherDropAmount = 1;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        for (int i = 0; i < featherDropAmount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            Instantiate(featherPrefab, transform.position + randomOffset, Quaternion.identity);
        }
        // Логика смерти врага
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Если враг сталкивается с игроком, наносим урон
        if (other.CompareTag("MainGoose"))
        {
            MainGoose goose = other.GetComponent<MainGoose>();
            if (goose != null)
            {
                goose.TakeDamage(damage + (damage*damageIncrease));
            }
        }
    }
}