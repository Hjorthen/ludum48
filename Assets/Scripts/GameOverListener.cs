using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverListener : MonoBehaviour
{
    public GameObject scorescreen;
    public Text scoreText;

    private void OnDestroy()
    {
        scoreText.text = "YOU DIED\n\nSCORE: " + Mathf.FloorToInt(Score.score);
        scorescreen.SetActive(true);
    }
}