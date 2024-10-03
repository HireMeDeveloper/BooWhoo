using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Components
    PlayerInput playerInput;
    Rigidbody2D rb;

    float speed = 5.0f;
    float jumpForce = 500.0f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerInput.actions["move"].IsPressed()) {
            PlayerMove();
        } else {
            // Stop moving in x direction if nothing is pressed
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (playerInput.actions["jump"].WasPressedThisFrame()) {
            PlayerJump();
        }
    }

    void PlayerMove() {
        var moveDirection = playerInput.actions["move"].ReadValue<Vector2>();
        // We want only the x value from move since there is a separate button to jump
        float xVal = moveDirection.x;
        rb.velocity = new Vector2(xVal * speed, rb.velocity.y);
    }

    void PlayerJump() {
        rb.AddForce(new Vector2(0, jumpForce));
    }
}
