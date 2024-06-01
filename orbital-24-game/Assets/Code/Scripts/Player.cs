using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }
    public Rigidbody2D playerRb;
    public float speed;
    public float input; // keep track of which directional button
    public SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable?.Interact(this);
        }
    }

    // Like update but runs 50 times per second only
    void FixedUpdate()
    {
        playerRb.velocity = new Vector2 (input * speed, playerRb.velocity.y);
    }
}
