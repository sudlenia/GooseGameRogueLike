using UnityEngine;
using System.Collections.Generic;

public class MainGoose : Character
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


    public Dictionary<int, string> weapons = new Dictionary<int, string>{
        { 1, "Пистолет" },
        { 2, "Молоток" },
        { 3, "Автомат" },
    };

    public Weapon currentWeapon;

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

    public virtual void Die()
    {
        // переход на главный экран
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

        currentWeapon = weapons[currentWeaponIndex];
    }

    //Сбор перьев
    private void OnTriggerEnter(Collider other)
    {
        // Если гусь сталкивается с пером, то собирается
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
            case "Урон":
                baseDamage += baseDamage * 0.1f;
                break;
            case "Скорость стрельбы":
                baseАireRate += baseАireRate * 0.1f;
                break;
            case "Скорость передвижения":
                speed += speed * 0.1f;
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