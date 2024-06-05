using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    private Vector3 StartingPos = Vector3.zero;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    private Vector2 MovePos;

    [SerializeField] private float maxX = 2;
    [SerializeField] private float maxY = 2;
    [SerializeField] private float minX = -2;
    [SerializeField] private float minY = -2;

    void Start()
    {
        transform.position = StartingPos;
        MovePos = StartingPos;
    }

    // Like update but runs 50 times per second only
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * sensitivity;
        float vertical = Input.GetAxis("Vertical") * sensitivity;

        MovePos.x += horizontal;
        MovePos.y += vertical;

        MovePos.x = Mathf.Clamp(MovePos.x, minX, maxX);
        MovePos.y = Mathf.Clamp(MovePos.y, minY, maxY);

        transform.position = Vector2.Lerp(transform.position, MovePos, speed * Time.deltaTime);
    }
}
