using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleStrategyWatcher : MonoBehaviour
{
    [SerializeField] private GameObject[] strategyTexts;
    [SerializeField] private BoolVariable isFreezeTurn;
    public void EnableStrategyTexts()
    {
        StartCoroutine(EnableStrategyTextsEnum());
    }

    private IEnumerator EnableStrategyTextsEnum()
    {
        while (isFreezeTurn.Value) 
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
        while (isFreezeTurn.Value) 
        {
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < strategyTexts.Length; i++)
        {
            strategyTexts[i].SetActive(false);
        }
    }
}
