using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor.PackageManager;

public class MainGoose : Entity
{
    [SerializeField]
    [Tooltip("Health")]
    public float health = 100;
    [SerializeField]
    [Tooltip("Level")]
    public int level = 1;
    [SerializeField]
    [Tooltip("Feathers")]
    public int experience = 0;
    [SerializeField]
    [Tooltip("Speed")]
    public float speed = 1f;
    [Tooltip("Damage")]
    public float baseDamage = 1f;
    [Tooltip("Rate of fire")]
    public float baseAireRate = 1f;

    private Vector2 direction;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    bool facingLeft = true;
    private Animator animX;
    private Animator animY;
    public Joystick joystick;

    public List<GameObject> weapons;

    public GameObject currentWeapon;

    private int currentWeaponIndex = 0;

    private int countOfOpenWeapons = 2;

    public GameObject MachineGun;

    private int[] feathersRequired = { 10, 15, 25, 40, 50, 50, 60, 75, 100 };
    [Tooltip("Feathers needed")]
    public int feathersToUp;

    private Dictionary<int, string> levelBonuses = new Dictionary<int, string>
    {
        { 2, "Damage" },
        { 3, "Rate of fire" },
        { 4, "Speed" },
        { 5, "Speed" },
        { 6, "Damage" },
        { 7, "Machine gun" },
        { 8, "Rate of fire" },
        { 9, "Damage" },
        { 10, "Damage" },
    };

    private void Awake()
    {
        if (DataHolder.stats == null)
        {
            DataHolder.stats = new List<float>()
            { health, level, experience,
            speed, baseDamage, baseAireRate, currentWeaponIndex};

        }

        health = DataHolder.stats[0];
        level = (int)DataHolder.stats[1];
        experience = (int)DataHolder.stats[2];
        speed = DataHolder.stats[3];
        baseDamage = DataHolder.stats[4];
        baseAireRate = DataHolder.stats[5];
        currentWeaponIndex = (int)DataHolder.stats[6];
    }
    private void Start()
    {
        feathersToUp = feathersRequired[level - 1];

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animX = GetComponent<Animator>();
        animY = GetComponent<Animator>();

        weapons[0].SetActive(false);
        currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.SetActive(true);
    }

    private void Update()
    {
        // if (Input.GetButtonDown("SwitchWeapon"))
        // {
        //     SwitchWeapon();
        // }
    }

    private void FixedUpdate() {
        Move();
    }

    public override void Move()
    {
        direction.x = joystick.Horizontal;
        direction.y = joystick.Vertical;
        animX.SetFloat("moveX", Mathf.Abs(direction.x));
        animY.SetFloat("moveY", Mathf.Abs(direction.y));
        rb.velocity = new Vector2(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime);
        if (!facingLeft && direction.x < 0){
            Flip();
        } else if (facingLeft && direction.x > 0) {
            Flip();
        }
    }

    private void Flip() {
        facingLeft = !facingLeft;
        sr.flipX = !sr.flipX;
    }


    public override void Die()
    {
        SceneManager.LoadScene(0);
        DataHolder.stats = null;
        DataHolder.damageIncrease = 0;
    }

    public void SwitchWeapon()
    {
        currentWeaponIndex++;
        DataHolder.stats[6]++;

        if (currentWeaponIndex >= countOfOpenWeapons)
        {
            currentWeaponIndex = 0;
            DataHolder.stats[6] = 0;
        }

        currentWeapon.SetActive(false);

        currentWeapon = weapons[currentWeaponIndex];

        currentWeapon.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Feather"))
        {
            CollectFeather();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Weapon"))
        {
            countOfOpenWeapons = 3;

            currentWeapon.SetActive(false);

            currentWeapon = weapons[2];

            currentWeapon.SetActive(true);

            Destroy(other.gameObject);
        }
    }

    public void GetDamage(float amount)
    {
        StartCoroutine(GooseOnAttack());
        health -= amount;
        DataHolder.stats[0] -= amount;
        if (health <= 0) Die();
    }
    private IEnumerator GooseOnAttack()
    {
        sr.color = new Color(1f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.2f);
        sr.color = new Color(1, 1, 1);
    }

    public void CollectFeather()
    {
        experience++;
        DataHolder.stats[2]++;

        if (experience == feathersToUp && level != 10)
        {
            feathersToUp = feathersRequired[level];

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
            case "Damage":
                baseDamage += baseDamage * 0.1f;
                DataHolder.stats[4] += baseDamage * 0.1f;
                break;
            case "Rate of fire":
                baseAireRate += baseAireRate * 0.1f;
                DataHolder.stats[5] += baseAireRate * 0.1f;
                break;
            case "Speed":
                speed += speed * 0.1f;
                DataHolder.stats[3] += speed * 0.1f;
                break;
            case "Machine gun":
                Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
                Instantiate(MachineGun, transform.position + randomOffset, Quaternion.identity);
                break;
            default:
                Debug.LogWarning($"Error");
                break;
        }
    }
}

public static class DataHolder
{
    public static List<float> stats;
    public static float damageIncrease = 0;
}