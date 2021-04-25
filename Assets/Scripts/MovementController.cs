using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float topSpeedMultiplier = 4;
    public Transform movementTarget;
    private new Rigidbody rigidbody;
    private Destination currentDestination;

    [System.Serializable]
    private struct Destination {
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

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        ClearDestination();
    }

    public void SetDestination(Vector3 destination)
    {
        destination.y = movementTarget.position.y;
        currentDestination = new Destination{
            isValid = true,
            location = destination
        };
    }

    public void ClearDestination()
    {
        currentDestination = new Destination() {
            isValid = false,
            location = Vector3.zero
        };
    }


    void Update()
    {
        if(currentDestination.isValid)
        {
            MoveTowardsDestination();
            StopMovementIfWithinTreshold();
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void MoveTowardsDestination()
    {
        Vector3 targetDestination = currentDestination.location;
        targetDestination.y = movementTarget.position.y;
        Vector3 direction = targetDestination - movementTarget.position;
        rigidbody.velocity = direction.normalized * topSpeedMultiplier;
    }

    private void StopMovementIfWithinTreshold()
    {
        float distance = Vector3.Distance(currentDestination.location, movementTarget.position);
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
}
