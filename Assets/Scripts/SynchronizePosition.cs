using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizePosition : MonoBehaviour
{
    public Transform synchronizeTo;
    private float latestXUpdate;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 updatePosition = new Vector3(Mathf.Max(latestXUpdate, currentPosition.x), currentPosition.y, currentPosition.z);
            synchronizeTo.position = updatePosition;
            transform.position = currentPosition;
        latestXUpdate = Mathf.Max(latestXUpdate, currentPosition.x);


    }
}
