using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 10;
    public bool destroyAfterHitFactor = false; // Destroy projectile after hitFactor reached
    public int hitFactor = 0;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit() // When projectile hits an Object
    {
        if (destroyAfterHitFactor && hitFactor <= 0)
            Destroy(gameObject);
        hitFactor--;
    }
}
