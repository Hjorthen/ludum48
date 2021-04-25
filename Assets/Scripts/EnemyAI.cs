using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class EnemyAI : MonoBehaviour
{
    MovementController movementController;    
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movementController = GetComponent<MovementController>();
    }

    void Update()
    {
        if(player != null)
            movementController.SetDestination(player.transform.position);
    }
}
