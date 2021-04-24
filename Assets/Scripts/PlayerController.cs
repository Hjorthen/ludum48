using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        if(Input.GetAxis("Move") > 0)
        {
            Destination newLocation =  GetClickedLocation();        
            if(newLocation.isValid)
            {
                currentDestination = newLocation;
            }
        }
        else
        {
            if(currentDestination.isValid)
            {
                float distance = Vector3.Distance(currentDestination.location, transform.position);
                Debug.Log(distance);
                if(distance < 1.5)
                {
                    currentDestination = Destination.Invalid;
                }
            }
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
