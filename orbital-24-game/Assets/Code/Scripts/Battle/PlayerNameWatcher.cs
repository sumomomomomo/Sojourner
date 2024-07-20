using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerNameWatcher : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private StringReference playerName;
    void Update()
    {
        playerNameText.text = playerName.Value;
    }
}
