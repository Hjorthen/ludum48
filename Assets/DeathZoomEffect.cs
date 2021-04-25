using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoomEffect : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 6, Time.deltaTime);        
    }
}
