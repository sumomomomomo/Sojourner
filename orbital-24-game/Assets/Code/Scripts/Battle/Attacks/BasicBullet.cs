using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField] private float damageModifier;
    [SerializeField] private bool isBlue;
    [SerializeField] private bool isOrange;
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerDamageTaker playerDamageTaker = other.GetComponentInChildren<PlayerDamageTaker>();
        if (other.CompareTag("Player") && playerDamageTaker != null)
        {
            if (isBlue && playerDamageTaker.IsPlayerMoving.Value)
            {
                //Debug.Log("blue damage!");
                playerDamageTaker.takeDamage(damageModifier);
            }
            else if (isOrange && !playerDamageTaker.IsPlayerMoving.Value)
            {
                //Debug.Log("orange damage!");
                playerDamageTaker.takeDamage(damageModifier);
            }
            else if (!isOrange && !isBlue)
            {
                //Debug.Log("normal damage!");
                playerDamageTaker.takeDamage(damageModifier);
            }
        }
    }

}
