using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleStrategyWatcher : MonoBehaviour
{
    [SerializeField] private TMP_Text strategyText;
    [SerializeField] private BattleStrategyTrackerObject currentStrategy;
    void Update()
    {
        strategyText.text = "Strategy: " + currentStrategy.StrategyName;
    }
}
