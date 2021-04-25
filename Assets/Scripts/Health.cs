using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    void Start()
    {
    }

    void Update()
    {
        if (health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
    }
}
