using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float topSpeedMultiplier = 4;
    public PlayerController playerController;
    private Rigidbody rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.currentDestination.isValid)
        {
            Vector3 targetDestination = playerController.currentDestination.location;
            targetDestination.y = playerController.transform.position.y;
            Vector3 direction = targetDestination - playerController.transform.position;
            rigidbody.velocity = direction.normalized * topSpeedMultiplier;
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}
