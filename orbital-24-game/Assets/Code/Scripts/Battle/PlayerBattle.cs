using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    [SerializeField] private FloatReference playerSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoolReference isFreezeTurn;

    void Start()
    {
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (isFreezeTurn.Value)
        {
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
        }
    }
}
