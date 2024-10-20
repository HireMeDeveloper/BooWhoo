using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Components
    PlayerInput playerInput;
    PlayerInteraction playerInteraction;
    Rigidbody2D rb;
    BoxCollider2D bc;

    float speed = 5.0f;
    float jumpForce = 400.0f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInteraction = GetComponent<PlayerInteraction>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (playerInput.actions["move"].IsPressed()) {
            PlayerMove();
        } else {
            // Stop moving in x direction if nothing is pressed
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (playerInput.actions["jump"].WasPressedThisFrame() && IsGrounded()) {
            PlayerJump();
        }

        if (playerInput.actions["Interact"].WasPressedThisFrame() && playerInteraction.hoveredInteractables.Count > 0) {
            playerInteraction.Interact();
        }
    }

    void PlayerMove() {
        var moveDirection = playerInput.actions["move"].ReadValue<Vector2>();
        // We want only the x value from move since there is a separate button to jump
        float xVal = moveDirection.x;
        rb.velocity = new Vector2(xVal * speed, rb.velocity.y);
    }

    void PlayerJump() {
        AudioManager.CreateAudio("Jump");
        rb.AddForce(new Vector2(0, jumpForce));
    }

    bool IsGrounded() {
        // Need to check raycast at edges of player or jumping while hanging off a ledge will fail
        var leftRay = Physics2D.Raycast((Vector2)transform.position + new Vector2(-bc.size.x/2, 0), Vector2.down, 0.1f);
        var rightRay = Physics2D.Raycast((Vector2)transform.position + new Vector2(bc.size.x/2, 0), Vector2.down, 0.1f);
        if (rb.velocity.y == 0 && (leftRay || rightRay)) {
            return true;
        }
        return false;
    }

}
