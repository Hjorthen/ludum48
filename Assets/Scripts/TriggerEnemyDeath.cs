using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemyDeath : MonoBehaviour
{
    private void OnDestroy()
    {
        Score.IncrementKills();
    }
}
