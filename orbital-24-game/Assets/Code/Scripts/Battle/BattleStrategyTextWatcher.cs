using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BattleStrategyTextWatcher : MonoBehaviour
{
    [SerializeField] private BattleStrategyTrackerObject currentStrategy;
    [SerializeField] private BattleStrategyObject referenceStrategy;
    [SerializeField] private TMP_Text selfText;

    void Update()
    {
        if (referenceStrategy.StrategyName == currentStrategy.StrategyName)
        {
            selfText.fontStyle = FontStyles.Bold;
        }
        else
        {
            selfText.fontStyle = FontStyles.Normal;
        }
    }
}
