using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] private FloatReference playerSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BattleState battleState;
    [SerializeField] private BoolVariable isPlayerMoving;

    void Start()
    {
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (battleState.IsPlayerUnmovable())
        {
            isPlayerMoving.Value = false;
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            float currSpeed = playerSpeed.Value;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currSpeed /= 2;
            }

            float horizontal = Input.GetAxisRaw("Horizontal") * currSpeed;
            float vertical = Input.GetAxisRaw("Vertical") * currSpeed;
            rb.velocity = new Vector2(horizontal, vertical);
            if (rb.velocity != Vector2.zero)
            {
                isPlayerMoving.Value = true;
            }
            else
            {
                isPlayerMoving.Value = false;
            }
        }
    }
}
