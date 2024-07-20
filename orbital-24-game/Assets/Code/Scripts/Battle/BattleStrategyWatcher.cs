using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleStrategyWatcher : MonoBehaviour
{
    [SerializeField] private GameObject[] strategyTexts;
    [SerializeField] private BattleState battleState;
    public void EnableStrategyTexts()
    {
        StartCoroutine(EnableStrategyTextsEnum());
    }

    private IEnumerator EnableStrategyTextsEnum()
    {
        while (!battleState.IsPlayerStrategySelectable()) 
        {
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < strategyTexts.Length; i++)
        {
            strategyTexts[i].SetActive(true);
        }
    }

    public void DisableStrategyTexts()
    {
        StartCoroutine(DisableStrategyTextsEnum());
    }

    private IEnumerator DisableStrategyTextsEnum()
    {        
        while (!battleState.IsPlayerStrategySelectable()) 
        {
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < strategyTexts.Length; i++)
        {
            strategyTexts[i].SetActive(false);
        }
    }
}
