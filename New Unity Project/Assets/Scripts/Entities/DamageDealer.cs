using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] bool destroyAfterHitFactor = false; // Destroy projectile after hitFactor reached
    [SerializeField] int hitFactor = 0;

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
