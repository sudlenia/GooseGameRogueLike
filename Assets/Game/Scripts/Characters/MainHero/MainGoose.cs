using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    public List<GameObject> weapons;

    public GameObject currentWeapon;

    private int currentWeaponIndex = 0;

    private int[] feathersRequired = { 10, 15, 25, 40, 50, 50, 60, 75, 100 };
    [Tooltip("Feathers needed")]
    public int feathersToUp;

    private Dictionary<int, string> levelBonuses = new Dictionary<int, string>
    {
        { 2, "Damage" },
        { 3, "Rate of fire" },
        { 4, "Hammer" },
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
            speed, baseDamage, baseAireRate};
        }
        if (DataHolder.weapons == null)
        {
            DataHolder.weapons = weapons;
        }

        health = DataHolder.stats[0];
        level = (int)DataHolder.stats[1];
        experience = (int)DataHolder.stats[2];
        speed = DataHolder.stats[3];
        baseDamage = DataHolder.stats[4];
        baseAireRate = DataHolder.stats[5];

        weapons = DataHolder.weapons;
    }
    private void Start()
    {
        feathersToUp = feathersRequired[0];

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animX = GetComponent<Animator>();
        animY = GetComponent<Animator>();
        currentWeapon = weapons[currentWeaponIndex];
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

    private void Move()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        animX.SetFloat("moveX", Mathf.Abs(direction.x));
        animY.SetFloat("moveY", Mathf.Abs(direction.y));
        rb.velocity = new Vector2(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime);
        if (facingLeft == false && direction.x < 0){
            Flip();
        } else if (facingLeft == true && direction.x > 0) {
            Flip();
        }
    }

    private void Flip() {
        facingLeft = !facingLeft;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


    public override void Die()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchWeapon()
    {
        currentWeaponIndex++;

        if (currentWeaponIndex >= weapons.Count)
        {
            currentWeaponIndex = 0;
        }

        currentWeapon.SetActive(false);

        currentWeapon = weapons[currentWeaponIndex];

        currentWeapon.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Feather"))
        {
            CollectFeather();
            Destroy(other.gameObject);
        }
    }


    public void CollectFeather()
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
            case "Damage":
                baseDamage += baseDamage * 0.1f;
                DataHolder.stats[4]++;
                break;
            case "Rate of fire":
                baseAireRate += baseAireRate * 0.1f;
                DataHolder.stats[5]++;
                break;
            case "Speed":
                speed += speed * 0.1f;
                DataHolder.stats[3]++;
                break;
            case "Hammer":
                //Add spawn hammer
                break;
            case "Machine gun":
                //Add spawn machine gun
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

    public static List<GameObject> weapons;
}