using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    // For correctly determining bounds
    private Camera mainCamera;
    private Vector3 StartingPos = Vector3.zero;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    private Vector2 MovePos;

    [SerializeField] private FloatReference borderThickness;

    [SerializeField] private FloatReference boundWidth;
    [SerializeField] private FloatReference boundHeight;
    [SerializeField] private FloatReference boundOriginXTranslation;
    [SerializeField] private FloatReference boundOriginYTranslation;

    //private Vector3 transformScreenPosition;

    private Vector3 worldBoundsTopRight;
    private Vector3 worldBoundsBottomLeft;
    private Vector3 worldBoundsZero;

    void Start()
    {
        transform.position = StartingPos;
        MovePos = StartingPos;
        mainCamera = Camera.main;
        worldBoundsZero = mainCamera.WorldToScreenPoint(new Vector3(boundOriginXTranslation.Value, boundOriginYTranslation.Value, 0));
        Debug.Log(worldBoundsZero);
    }

    // Like update but runs 50 times per second only
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * sensitivity;
        float vertical = Input.GetAxis("Vertical") * sensitivity;

        MovePos.x += horizontal;
        MovePos.y += vertical;

        //transformScreenPosition = mainCamera.WorldToScreenPoint(transform.position) - mainCamera.WorldToScreenPoint(new Vector3(0, 0, 0));

        worldBoundsTopRight = mainCamera.ScreenToWorldPoint(new Vector3(
            worldBoundsZero.x + boundWidth.Value / 2, 
            worldBoundsZero.y + boundHeight.Value / 2, 
            -10));
        worldBoundsBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(
            worldBoundsZero.x - boundWidth.Value / 2, 
            worldBoundsZero.y - boundHeight.Value / 2, 
            -10));

        MovePos.x = Mathf.Clamp(MovePos.x, worldBoundsBottomLeft.x, worldBoundsTopRight.x);
        MovePos.y = Mathf.Clamp(MovePos.y, worldBoundsBottomLeft.y, worldBoundsTopRight.y);

        transform.position = Vector2.Lerp(transform.position, MovePos, speed * Time.deltaTime);
    }
}
