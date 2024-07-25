using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusDisplayScreen : MonoBehaviour
{
    [SerializeField] private StringVariable playerName;
    [SerializeField] private IntVariable playerAtk;
    [SerializeField] private IntVariable playerDef;
    [SerializeField] private FloatVariable playerAgi;
    [SerializeField] private IntVariable playerLvl;
    [SerializeField] private IntVariable playerMoney;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text atkText;
    [SerializeField] private TMP_Text defText;
    [SerializeField] private TMP_Text agiText;
    [SerializeField] private TMP_Text lvlText;
    [SerializeField] private TMP_Text moneyText;

    void Update()
    {
        nameText.text = playerName.Value;
        atkText.text = playerAtk.Value.ToString();
        defText.text = playerDef.Value.ToString();
        agiText.text = playerAgi.Value.ToString();
        lvlText.text = playerLvl.Value.ToString();
        moneyText.text = playerMoney.Value.ToString();
    }                
}
