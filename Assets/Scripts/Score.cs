using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float score;
    public GameObject player;
    private static int enemiesKilled;

    private void Start()
    {
        score = 0;
        enemiesKilled = 0;
    }

    private void Update()
    {
        score = player.transform.position.x + (enemiesKilled * 10);
    }
    public static void IncrementKills()
    {
        enemiesKilled += 1;
    }
}