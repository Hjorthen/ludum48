using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtable : MonoBehaviour
{
    private Health health;

    void Start()
    {
        health = GetComponent<Health>();
    }

    private void OnParticleCollision(GameObject other)
    {
        health.Damage(0.1f);
    }
}
