using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;


    public void Start()
    {
        slider.value = 100;
    }

    public void SetHealth(float health)
    {
        slider.value = health / 100;
    }

    public void Update()
    {
        if (player != null)
        {
            SetHealth(player.GetComponent<Health>().health);
        }
    }
}
