using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem flameThrower;
    public struct Destination {
        public bool isValid;
        public Vector3 location;

        public static Destination Invalid {
            get{
                return new Destination {
                    location = Vector2.zero,
                    isValid = false
                };
            }
        }
    }

    public Destination currentDestination{
        private set;
        get;
    }
    void Start()
    {
        currentDestination = Destination.Invalid;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Move") > 0 || Input.GetButton("AttackMove"))
        {
            UpdatePlayerDestination();
        }

        if(currentDestination.isValid)
        {
            StopMovementIfWithinTreshold();
            UpdatePlayerFacing();
        }


        if(Input.GetButtonDown("AttackMove"))
        {
            flameThrower.Play();
        }
        else if(Input.GetButtonUp("AttackMove"))
        {
            flameThrower.Stop();
        }
    }

    private void UpdatePlayerFacing()
    {

        transform.LookAt(currentDestination.location, Vector3.up);
    }

    private void UpdatePlayerDestination()
    {
        Destination newLocation = GetClickedLocation();
        newLocation.location.y = transform.position.y;
        if (newLocation.isValid)
        {
            currentDestination = newLocation;
        }
    }

    private void StopMovementIfWithinTreshold()
    {
        float distance = Vector3.Distance(currentDestination.location, transform.position);
        if (distance < 1.0)
        {
            currentDestination = Destination.Invalid;
        }
    }

    public void OnDrawGizmos()
    {
        if(currentDestination.isValid)
            Gizmos.DrawSphere(currentDestination.location, 1.0f);
    }

    private Destination GetClickedLocation()
    {
        Transform cameraTransform = Camera.main.transform;
        var clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var result = Physics.RaycastAll(ray, float.MaxValue, LayerMask.GetMask("Terrain"));
        if(result.Length > 0)
        {
            Destination dest = new Destination
            {
                location = result[0].point,
                isValid = true
            };
            return dest;
        }
        return Destination.Invalid;
    }
}
