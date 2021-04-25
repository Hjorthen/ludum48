using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    private float dealDamageNextPhysicsUpdate;
    private float dealDamageAmount;

    public void DealDamage(float amount)
    {
        dealDamageNextPhysicsUpdate += amount;
    }

    void OnFixedUpdate()
    {
        dealDamageAmount = dealDamageNextPhysicsUpdate;
        dealDamageNextPhysicsUpdate = 0;
    }

    void OnCollisionStay(Collision other)
    {
        var health = other.gameObject.GetComponent<Health>();
        if(health != null)
        {
            health.Damage(dealDamageAmount);
        }
    }
}
