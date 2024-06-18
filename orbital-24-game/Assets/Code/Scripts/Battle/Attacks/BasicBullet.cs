using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField] private FloatReference damageModifier;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDamageTaker playerDamageHandler = other.GetComponentInChildren<PlayerDamageTaker>();
        if (other.CompareTag("Player") && playerDamageHandler != null)
        {
            playerDamageHandler.takeDamage(damageModifier.Value);
        }
    }

}
