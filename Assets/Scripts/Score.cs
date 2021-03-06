using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static float score;
    public GameObject player;
    public Gradient MoodGradient;
    public Light DirectionalLight;
    private static int enemiesKilled;
    public int scorePerEnemy;
    public float playerMaxDepth{
        private set;
        get;
    }

    private void Start()
    {
        score = 0;
        enemiesKilled = 0;
    }

    private void UpdateMoodLight()
    {
        float ratio = Mathf.Clamp(playerMaxDepth / 800, 0, 1);
        DirectionalLight.color = MoodGradient.Evaluate(ratio);
        Camera.main.backgroundColor = MoodGradient.Evaluate(ratio);
    }

    private void Update()
    {
        if (player != null)
        {
            playerMaxDepth = Mathf.Max(player.transform.position.x, playerMaxDepth);
            score = Mathf.FloorToInt(playerMaxDepth) + (enemiesKilled * scorePerEnemy);
        }
        UpdateMoodLight(); 

    }
    public static void IncrementKills()
    {
        enemiesKilled += 1;
    }
}