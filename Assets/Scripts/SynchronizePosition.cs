using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizePosition : MonoBehaviour
{
    public Transform synchronizeTo;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        synchronizeTo.position = currentPosition;
        transform.position = currentPosition;
    }
}
