using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Damage")]
    public int damage;
    [SerializeField]
    [Tooltip("Range")]
    public float range;
    [SerializeField]
    [Tooltip("Rate of fire")]
    public float fireRate; 

    public virtual void Fire()
    {

    }
}