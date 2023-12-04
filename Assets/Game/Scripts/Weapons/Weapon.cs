using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Урон от попадания")]
    public int damage;
    [SerializeField]
    [Tooltip("Дальность стрельбы")]
    public float range;
    [SerializeField]
    [Tooltip("Скорость стрельбы")]
    public float fireRate; 

    public virtual void Fire()
    {
        // Логика выстрела общая для всех оружий
    }
}