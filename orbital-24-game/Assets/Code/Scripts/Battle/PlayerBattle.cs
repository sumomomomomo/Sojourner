using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D playerWallCollider;

    // private Vector2 prevPosition;

    //private ContactFilter2D wallFilter;
    // private Collider2D[] temp;

    //private bool isOverlapWall;


    void Start()
    {
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        // prevPosition = rb.position;
        // temp = new Collider2D[1];
        // wallFilter.minDepth = -1;
        // wallFilter.maxDepth = -1;
        // wallFilter.useDepth = true;
    }

    void FixedUpdate()
    {
        // isOverlapWall = Physics2D.OverlapCollider(playerWallCollider, wallFilter, temp) > 0;
        // if (isOverlapWall)
        // {
        //     rb.position = prevPosition;
        //     rb.velocity = Vector2.zero;
        // }
        // else
        // {
        // prevPosition.x = rb.position.x;
        // prevPosition.y = rb.position.y;

        float horizontal = Input.GetAxisRaw("Horizontal") * speed;
        float vertical = Input.GetAxisRaw("Vertical") * speed;

        //rb.MovePosition(new Vector2(rb.position.x + horizontal, rb.position.y + vertical));
        rb.velocity = new Vector2(horizontal, vertical);
        // }

        //transformScreenPosition = mainCamera.WorldToScreenPoint(transform.position) - mainCamera.WorldToScreenPoint(new Vector3(0, 0, 0));

        // worldBoundsTopRight = mainCamera.ScreenToWorldPoint(new Vector3(
        //     worldBoundsZero.x + boundWidth.Value / 2, 
        //     worldBoundsZero.y + boundHeight.Value / 2, 
        //     -10));
        // worldBoundsBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(
        //     worldBoundsZero.x - boundWidth.Value / 2, 
        //     worldBoundsZero.y - boundHeight.Value / 2, 
        //     -10));

        // MovePos.x = Mathf.Clamp(MovePos.x, worldBoundsBottomLeft.x, worldBoundsTopRight.x);
        // MovePos.y = Mathf.Clamp(MovePos.y, worldBoundsBottomLeft.y, worldBoundsTopRight.y);

        //transform.position = Vector2.Lerp(transform.position, MovePos, speed * Time.deltaTime);
    }

    // void LateUpdate()
    // {
    //     Debug.Log(prevPosition);
    //     bool isOverlapWall = Physics2D.OverlapCollider(playerWallCollider, wallFilter, temp) > 0;
    //     if (isOverlapWall)
    //     {
    //         rb.position = prevPosition;
    //         rb.velocity = Vector2.zero;
    //     }
    //     else
    //     {
    //         prevPosition.x = rb.position.x;
    //         prevPosition.y = rb.position.y;
    //     }
    // }
}
