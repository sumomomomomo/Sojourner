using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    [SerializeField]
    private TMP_Text textLabel;
    void Start()
    {
        textLabel.text = "Test\nTest2";
    }

    void Update()
    {
        
    }
}
