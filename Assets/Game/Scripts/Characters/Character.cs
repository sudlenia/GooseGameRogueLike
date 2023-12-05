using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public void GetDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
}
