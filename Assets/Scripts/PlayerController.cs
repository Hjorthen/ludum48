using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerController : MonoBehaviour
{
    public ParticleSystem flameThrower;
    private MovementController movementController;
    public GameObject holyHandgrenadePrefab;


    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            DeployTheMightyHandgrenade();
        }
        if(Input.GetAxis("Move") > 0)
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

        UpdatePlayerFacing();
    }

    private void DeployTheMightyHandgrenade()
    {
        if(GetCursorWorldLocation(out Vector3 location))
        {
            location.y = transform.position.y;
            Vector3 direction = (location - transform.position).normalized;
            var theNade = GameObject.Instantiate(holyHandgrenadePrefab, transform.position, Quaternion.identity);
            var rigidBody = theNade.GetComponent<Rigidbody>();
            rigidBody.AddForce(direction * 10, ForceMode.VelocityChange);
        }
    }

    private void UpdatePlayerFacing()
    {
        if(GetCursorWorldLocation(out Vector3 location))
        {
            transform.LookAt(location);
        }
    }

    private void UpdatePlayerDestination()
    {
        if(GetCursorWorldLocation(out Vector3 destination))
        {
            movementController.SetDestination(destination);
        }
        else
        {
            movementController.ClearDestination();
        }
    }

    private bool GetCursorWorldLocation(out Vector3 location)
    {

        Transform cameraTransform = Camera.main.transform;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var result = Physics.RaycastAll(ray, float.MaxValue, LayerMask.GetMask("Terrain"));
        if(result.Length > 0)
        {
            location = result[0].point;
            return true;
        }
        location = Vector3.zero;
        return false;
    }
}
