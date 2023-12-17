using UnityEngine;

public abstract class Enemy : Entity
{
    [SerializeField]
    [Tooltip("��������")]
    public float health;

    [SerializeField]
    [Tooltip("����")]
    public float damage;

    [SerializeField]
    [Tooltip("�������� ������������")]
    public float speed;

    [SerializeField]
    [Tooltip("���������� ����� � �������� �����")]
    public float damageIncrease = 0.1f;


    [SerializeField]
    [Tooltip("������ ����")]
    private GameObject featherPrefab;
    [SerializeField]
    [Tooltip("���������� ���������� ������")]
    public int featherDropAmount = 1;


    public override void Die()
    {
        for (int i = 0; i < featherDropAmount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            Instantiate(featherPrefab, transform.position + randomOffset, Quaternion.identity);
        }
        // ������ ������ �����
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� ���� ������������ � �������, ������� ����
        if (other.CompareTag("MainGoose"))
        {
            MainGoose goose = other.GetComponent<MainGoose>();
            if (goose != null)
            {
                goose.GetDamage(damage + (damage*damageIncrease), health);
            }
        }
    }
}