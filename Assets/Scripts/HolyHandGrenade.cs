using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyHandGrenade : MonoBehaviour
{
    public ParticleSystem[] InFlightEffects;
    public ParticleSystem[] OnHitEffects;
    public DamageArea Area;

    public float Damage;
    private bool destroyOnFinished;
    private new Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
    }
    public void Update()
    {
        if(destroyOnFinished)
        {
            bool shouldDestroy = true;
            foreach (var item in OnHitEffects)
            {
                if(!item.isStopped)
                {
                    shouldDestroy = false;
                    break;
                }   
            }
            if(shouldDestroy)
                GameEvents.Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        foreach (var item in InFlightEffects)
        {
            item.Stop(false);   
        }
        foreach (var item in OnHitEffects)
        {
            item.Play();   
        }
        Area.DealDamage(Damage);
        collider.enabled = false;
        destroyOnFinished = true;
    }
}
