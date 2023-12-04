using UnityEngine;

public class Hammer : Weapon
{
    public Hammer()
    {
        damage = 50;
        range = 1f;
        fireRate = 1.5f;
    }
    public override void Fire()
    {
        // Логика удара молотком
    }
}
