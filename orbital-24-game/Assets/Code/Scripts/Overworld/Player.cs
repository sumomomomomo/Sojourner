using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float speed;
    [SerializeField] private float input; // keep track of which directional button
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private OverworldInputHandlerStateObject overworldInputHandlerStateObject;

    // Update is called once per frame
    void Update()
    {
        if (overworldInputHandlerStateObject.CanPlayerMove())
        {
            input = Input.GetAxisRaw("Horizontal");
            if (input < 0) 
            {
                spriteRenderer.flipX = true;
            }
            else if (input > 0) 
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            input = 0;
        }

        if (overworldInputHandlerStateObject.CanPlayerInteract())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interactable?.Interact(this);
            }
        }
    }

    // Like update but runs 50 times per second only
    void FixedUpdate()
    {
        playerRb.velocity = new Vector2 (input * speed, playerRb.velocity.y);
    }
}
