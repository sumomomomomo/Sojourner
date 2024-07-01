using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStrategyExecuteDebugText : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText;
    private float timeToDisappear;

    void Start()
    {
        debugText.text = "";
    }

    void Update()
    {
        if (timeToDisappear <= 0) return;
        timeToDisappear -= Time.deltaTime;
        if (timeToDisappear <= 0)
        {
            debugText.text = "";
        }
    }

    public void saySomething(string something)
    {
        timeToDisappear = 1;
        debugText.text = something;
    }
}
