using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float score;
    public GameObject player;
    private static int enemiesKilled;
    private float playerMaxDepth;

    private void Start()
    {
        score = 0;
        enemiesKilled = 0;
    }

    private void Update()
    {
        playerMaxDepth = Mathf.Max(player.transform.position.x, playerMaxDepth);
        score = Mathf.FloorToInt(playerMaxDepth + (enemiesKilled * 10));
    }
    public static void IncrementKills()
    {
        enemiesKilled += 1;
    }
}