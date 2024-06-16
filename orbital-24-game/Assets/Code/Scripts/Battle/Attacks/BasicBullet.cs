using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerDamageHandler playerDamageHandler))
        {
            playerDamageHandler.takeDamage(2);
        }
    }

}
