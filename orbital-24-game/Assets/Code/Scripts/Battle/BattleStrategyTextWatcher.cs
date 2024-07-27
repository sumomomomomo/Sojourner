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
        selfText.text = referenceStrategy.DisplayText;
        if (referenceStrategy.StrategyName == currentStrategy.StrategyName)
        {
            selfText.fontStyle = FontStyles.Bold;
            selfText.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (referenceStrategy.IsDisabled)
        {
            selfText.fontStyle = FontStyles.Normal;
            selfText.color = new Color(1f, 1f, 1f, 0.2f);
        }
        else
        {
            selfText.fontStyle = FontStyles.Normal;
            selfText.color = referenceStrategy.Color;
        }
    }
}
