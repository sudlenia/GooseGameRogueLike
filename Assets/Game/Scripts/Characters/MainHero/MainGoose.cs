using UnityEngine;
using System.Collections.Generic;

public class MainGoose : Character
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


    public Dictionary<int, string> weapons = new Dictionary<int, string>{
        { 1, "��������" },
        { 2, "�������" },
        { 3, "�������" },
    };

    public Weapon currentWeapon;

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

    private void Start()
    {
        feathersToUp = feathersRequired[0];
        weapons = new List<Weapon>
        {
            new Pistol()
        };

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

    public virtual void Die()
    {
        // ������� �� ������� �����
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

        currentWeapon = weapons[currentWeaponIndex];
    }

    //���� ������
    private void OnTriggerEnter(Collider other)
    {
        // ���� ���� ������������ � �����, �� ����������
        if (other.CompareTag("Feather"))
        {
            Feather feather = other.GetComponent<Feather>();
            if (feather != null)
            {
                feather.CollectFeather();
                if (experience == feathersToUp && level != 10)
                {
                    feathersToUp = feathersRequired[level-1];
                    level++;
                    experience = 0;
                    if (levelBonuses.TryGetValue(level, out string bonus))
                    {
                        LevelUp(bonus);
                    }
                }
            }
        }
    }


    public void LevelUp(string bonus)
    {
        switch (bonus)
        {
            case "����":
                baseDamage += baseDamage * 0.1f;
                break;
            case "�������� ��������":
                base�ireRate += base�ireRate * 0.1f;
                break;
            case "�������� ������������":
                speed += speed * 0.1f;
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