using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerHealthWatcher : MonoBehaviour
{
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private IntReference playerHealth;
    [SerializeField] private IntReference playerMaxHealth;
    void Update()
    {
        playerHealthText.text = "Player: " + playerHealth.Value + "/" + playerMaxHealth.Value + " HP";
    }
}
