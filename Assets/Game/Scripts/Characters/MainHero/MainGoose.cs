using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainGoose : Entity
{
    [SerializeField]
    [Tooltip("��������")]
    public float health = 100;
    [SerializeField]
    [Tooltip("������� �������")]
    public int level = 1;
    [SerializeField]
    [Tooltip("���������� ������")]
    public int experience = 0;
    [SerializeField]
    [Tooltip("�������� ������������")]
    public float speed = 1f;
    [Tooltip("������� ����")]
    public float baseDamage = 1f;
    [Tooltip("������� �������� ��������")]
    public float base�ireRate = 1f;


    public List<GameObject> weapons;

    public GameObject currentWeapon;

    private int currentWeaponIndex = 0;

    private int[] feathersRequired = { 10, 15, 25, 40, 50, 50, 60, 75, 100 };
    [Tooltip("���������� ������ ��� ���������� ������")]
    public int feathersToUp;

    // ������� ��� �������� ������� ��� ������� ������
    private Dictionary<int, string> levelBonuses = new Dictionary<int, string>
    {
        { 2, "����" },
        { 3, "�������� ��������" },
        { 4, "�������" },
        { 5, "�������� ������������" },
        { 6, "����" },
        { 7, "�������" },
        { 8, "�������� ��������" },
        { 9, "����" },
        { 10, "����" },
    };

    private void Awake()
    {
        if (DataHolder.stats == null)
        {
            DataHolder.stats = new List<float>() 
            { health, level, experience,
            speed, baseDamage, base�ireRate};
        }
        if(DataHolder.weapons == null)
        {
            DataHolder.weapons = weapons;
        }

        health = DataHolder.stats[0];
        level = (int)DataHolder.stats[1];
        experience = (int)DataHolder.stats[2];
        speed = DataHolder.stats[3];
        baseDamage = DataHolder.stats[4];
        base�ireRate = DataHolder.stats[5];

        weapons = DataHolder.weapons;
    }
    private void Start()
    {
        feathersToUp = feathersRequired[0];
        

        currentWeapon = weapons[currentWeaponIndex];
    }

    private void Update()
    {
        // ����� ������������ � �������������� �����
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        // ������������ ������ (���� ������� ������ ����� �������� � ��������� - SwitchWeapon)
        if (Input.GetButtonDown("SwitchWeapon"))
        {
            SwitchWeapon();
        }
    }

    //������������
    private void Move(float horizontal, float vertical)
    {
        // ������ ������������
    }

    public override void Die()
    {
        SceneManager.LoadScene(0);
    }

    //����� ������
    public void SwitchWeapon()
    {
        // ��������� ������ �� ������
        currentWeaponIndex++;

        // ���� ������� �� ������� ������, ������������ � ������� ������
        if (currentWeaponIndex >= weapons.Count)
        {
            currentWeaponIndex = 0;
        }

        currentWeapon.SetActive(false);

        currentWeapon = weapons[currentWeaponIndex];

        currentWeapon.SetActive(true);
    }

    //���� ������
    private void OnTriggerEnter(Collider other)
    {
        // ���� ���� ������������ � �����, �� ����������
        if (other.CompareTag("Feather"))
        {
            CollectFeather();
            Destroy(other.gameObject);
        }
    }

    public void GetDamage(float amount)
    {
        health -= amount;
        DataHolder.stats[0] -= amount;
        if (health <= 0) Die();
    }

    private void CollectFeather()
    {
        experience++;
        DataHolder.stats[2]++;

        if (experience == feathersToUp && level != 10)
        {
            feathersToUp = feathersRequired[level - 1];

            level++;
            DataHolder.stats[1]++;

            experience = 0;
            DataHolder.stats[2] = 0;

            if (levelBonuses.TryGetValue(level, out string bonus))
            {
                LevelUp(bonus);
            }
        }
    }

    public void LevelUp(string bonus)
    {
        switch (bonus)
        {
            case "����":
                baseDamage += baseDamage * 0.1f;
                DataHolder.stats[4]++;
                break;
            case "�������� ��������":
                base�ireRate += base�ireRate * 0.1f;
                DataHolder.stats[5]++;
                break;
            case "�������� ������������":
                speed += speed * 0.1f;
                DataHolder.stats[3]++;
                break;
            case "�������":
                //�������� ����� �������
                break;
            case "�������":
                //�������� ����� �������� 
                break;
            default:
                Debug.LogWarning($"������");
                break;
        }
    }
}

public static class DataHolder
{
    public static List<float> stats;

    public static List<GameObject> weapons;
}