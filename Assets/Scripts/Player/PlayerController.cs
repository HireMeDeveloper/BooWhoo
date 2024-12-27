using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Components
    public Animator anim;
    public SpriteRenderer sr;
    PlayerInput playerInput;
    PlayerInteraction playerInteraction;
    Rigidbody2D rb;
    BoxCollider2D bc;

    float speed = 5.0f;
    float jumpForce = 400.0f;
    bool inAir = false;

    // Animation parameters
    bool isMoving = false;
    bool isJumping = false;
    float xDir = 0.0f;

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
            isMoving = true;
        } else {
            // Stop moving in x direction if nothing is pressed
            rb.velocity = new Vector2(0, rb.velocity.y);
            isMoving = false;
        }

        if (IsGrounded()) {
            if (playerInput.actions["jump"].WasPressedThisFrame()) {
                PlayerJump();
                isJumping = true;
            }
        } else {
            inAir = true;
        }

        if (playerInput.actions["Interact"].WasPressedThisFrame() && playerInteraction.hoveredInteractables.Count > 0) {
            playerInteraction.Interact();
        }

        Animate();
    }

    void PlayerMove() {
        var moveDirection = playerInput.actions["move"].ReadValue<Vector2>();
        // We want only the x value from move since there is a separate button to jump
        float xVal = moveDirection.x;
        rb.velocity = new Vector2(xVal * speed, rb.velocity.y);
        xDir = xVal;
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

    void Animate() {
        anim.SetFloat("xDir", xDir);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", isJumping);

        if (isJumping) {
            isJumping = false;
        }

        if (IsGrounded() && inAir) {
            anim.SetBool("isLanding", true);
            inAir = false;
        } else if (anim.GetBool("isLanding")) {
            anim.SetBool("isLanding", false);
        }

        // Flip sprite regardless of current animation
        if (xDir > 0) {
            sr.flipX = false;
        } else if (xDir < 0) {
            sr.flipX = true;
        }
    }

}
