using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    void Start()
    {
        GameEvents.current.onEnemyTriggerKilled += OnEnemyKilled;
    }

    private void OnEnemyKilled()
    {
        Score.IncrementKills();
    }
}