using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualizer : MonoBehaviour
{
    public Health health;
    public Color damageColor;
    public Animator animator;
    private float currentHealth;
    private Color baseColor;
    private MeshRenderer mesh;
    private int flicker = 0;
    private bool colorSwitch = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health.health;
        mesh = GetComponent<MeshRenderer>();
        baseColor = GetComponent<MeshRenderer>().material.color;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {





        if (currentHealth > health.health)
        {
            //animator.Play()
            flicker = 500;
        }

        /*
        if (flicker % 100 == 0 && flicker > 0)
        {
            mesh.material.color = damageColor;
            if (!colorSwitch)
            {
                colorSwitch = true;
            }
            else
            {
                colorSwitch = false;

            }
        }

        if (!colorSwitch)
        {
            mesh.material.color = baseColor;
        }
        else
        {
            mesh.material.color = damageColor;
        }

        if (flicker > 0)
        {
            flicker--;
        }
        else
        {
            mesh.material.color = baseColor;
        }
        */
        currentHealth = health.health;
    }
}
