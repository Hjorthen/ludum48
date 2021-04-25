using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualizer : MonoBehaviour
{
    public Health health;
    public Animator animator;
    private float currentHealth;

    void Start()
    {
        currentHealth = health.health;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
    }

    void LateUpdate()
    {

        if (currentHealth > health.health)
        {
            animator.SetBool("isDamaged", true);
        } else
        {
            animator.SetBool("isDamaged", false);
        }

        currentHealth = health.health;
    }
}