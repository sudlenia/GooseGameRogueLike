using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public void GetDamage(float amount, float health)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
    }
}
