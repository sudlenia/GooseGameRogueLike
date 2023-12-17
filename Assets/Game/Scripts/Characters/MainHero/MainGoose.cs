using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainGoose : Entity
{
    [SerializeField]
    [Tooltip("Здоровье")]
    public float health = 100;
    [SerializeField]
    [Tooltip("Текущий уровень")]
    public int level = 1;
    [SerializeField]
    [Tooltip("Количество перьев")]
    public int experience = 0;
    [SerializeField]
    [Tooltip("Скорость передвижения")]
    public float speed = 1f;
    [Tooltip("Базовый урон")]
    public float baseDamage = 1f;
    [Tooltip("Базовая скорость стрельбы")]
    public float baseАireRate = 1f;


    public List<GameObject> weapons;

    public GameObject currentWeapon;

    private int currentWeaponIndex = 0;

    private int[] feathersRequired = { 10, 15, 25, 40, 50, 50, 60, 75, 100 };
    [Tooltip("Количество перьев для следующего уровня")]
    public int feathersToUp;

    // Словарь для хранения бонусов для каждого уровня
    private Dictionary<int, string> levelBonuses = new Dictionary<int, string>
    {
        { 2, "Урон" },
        { 3, "Скорость стрельбы" },
        { 4, "Молоток" },
        { 5, "Скорость передвижения" },
        { 6, "Урон" },
        { 7, "Автомат" },
        { 8, "Скорость стрельбы" },
        { 9, "Урон" },
        { 10, "Урон" },
    };

    private void Awake()
    {
        if (DataHolder.stats == null)
        {
            DataHolder.stats = new List<float>() 
            { health, level, experience,
            speed, baseDamage, baseАireRate};
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
        baseАireRate = DataHolder.stats[5];

        weapons = DataHolder.weapons;
    }
    private void Start()
    {
        feathersToUp = feathersRequired[0];
        

        currentWeapon = weapons[currentWeaponIndex];
    }

    private void Update()
    {
        // Метод передвижения с использованием стика
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        // Переключение оружия (Надо создать кнопку чтобы работало в частности - SwitchWeapon)
        if (Input.GetButtonDown("SwitchWeapon"))
        {
            SwitchWeapon();
        }
    }

    //Передвижение
    private void Move(float horizontal, float vertical)
    {
        // Логика передвижения
    }

    public override void Die()
    {
        SceneManager.LoadScene(0);
    }

    //Смена оружия
    public void SwitchWeapon()
    {
        // следующее оружие по списку
        currentWeaponIndex++;

        // Если выходим за пределы списка, возвращаемся к первому оружию
        if (currentWeaponIndex >= weapons.Count)
        {
            currentWeaponIndex = 0;
        }

        currentWeapon.SetActive(false);

        currentWeapon = weapons[currentWeaponIndex];

        currentWeapon.SetActive(true);
    }

    //Сбор перьев
    private void OnTriggerEnter(Collider other)
    {
        // Если гусь сталкивается с пером, то собирается
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
            case "Урон":
                baseDamage += baseDamage * 0.1f;
                DataHolder.stats[4]++;
                break;
            case "Скорость стрельбы":
                baseАireRate += baseАireRate * 0.1f;
                DataHolder.stats[5]++;
                break;
            case "Скорость передвижения":
                speed += speed * 0.1f;
                DataHolder.stats[3]++;
                break;
            case "Молоток":
                //Добавить спавн молотка
                break;
            case "Автомат":
                //Добавить спавн автомата 
                break;
            default:
                Debug.LogWarning($"Ошибка");
                break;
        }
    }
}

public static class DataHolder
{
    public static List<float> stats;

    public static List<GameObject> weapons;
}