using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    bool playerDetected = false;

    // Update is called once per frame
    void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.F))
        {
            print("Dialogue");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.name == "Man") 
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        playerDetected = false;
    }
}
