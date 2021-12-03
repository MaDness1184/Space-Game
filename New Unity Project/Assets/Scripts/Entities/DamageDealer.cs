using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 10;
    public bool destroyAfterHitFactor = false; // Destroy projectile after hitFactor reached
    public int hitFactor = 0;

    // private
    private bool damageEnabler = true;

    public bool GetDamageEnabler()
    {
        return damageEnabler;
    }

    public int GetDamage()
    {
        if (damageEnabler)
            return damage;
        else
            return 0;
    }

    public void SetDamageEnabler(bool newBool)
    {
        damageEnabler = newBool;
    }

    public void Hit() // When projectile hits an Object
    {
        if (destroyAfterHitFactor && hitFactor <= 0)
            Destroy(gameObject);
        hitFactor--;
    }
}
