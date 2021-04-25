using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorescreen : MonoBehaviour
{
    public Text text;
    public GameObject scorescreen;

    private void Start()
    {
        scorescreen.SetActive(false);
    }

    public void ShowScorescreen(int score)
    {
        scorescreen.SetActive(true);
    }
}
