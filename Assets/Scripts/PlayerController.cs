using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerController : MonoBehaviour
{
    public ParticleSystem flameThrower;
    private MovementController movementController;


    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Move") > 0 || Input.GetButton("AttackMove"))
        {
            UpdatePlayerDestination();
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

    private void UpdatePlayerDestination()
    {
        Transform cameraTransform = Camera.main.transform;
        var clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var result = Physics.RaycastAll(ray, float.MaxValue, LayerMask.GetMask("Terrain"));
        if(result.Length > 0)
        {
            movementController.SetDestination(result[0].point);
        }
        else
        {
            movementController.ClearDestination();
        }
    }
}
