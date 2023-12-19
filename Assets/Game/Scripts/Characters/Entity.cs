using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    //public virtual void GetDamage(float amount, float health)
    //{
    //    health -= amount;
    //    if (health <= 0)
    //    {
    //        Die();
    //    }
    //}

    public abstract void Die();
}
