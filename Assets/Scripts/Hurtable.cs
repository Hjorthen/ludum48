using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    private Health health;
    public float damagePerParticle;

    void Start()
    {
        health = GetComponent<Health>();
    }

    private void OnParticleCollision(GameObject other)
    {
        health.Damage(damagePerParticle);
    }
}
