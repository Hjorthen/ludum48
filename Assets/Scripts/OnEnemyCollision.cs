using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyCollision : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            GetComponent<Health>().Damage(1);
        }
    }
}
