using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float score = 0;
    private int enemiesKilled = 0;

    private void Update()
    {
        score = transform.position.x;
    }
    public void IncrementKills()
    {
        enemiesKilled += 1;
    }
}
