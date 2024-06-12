using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStrategyInputHandler : MonoBehaviour
{
    [SerializeField] private BattleStrategyTrackerObject currentStrategy;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentStrategy.ToNextStrategy();
        }
    }
}
