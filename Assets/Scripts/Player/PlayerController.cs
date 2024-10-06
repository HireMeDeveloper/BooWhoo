using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Components
    PlayerInput playerInput;
    Rigidbody2D rb;
    BoxCollider2D bc;

    // This is the object the player is currently on top of.
    GameObject hoveredObject = null;
    // The item we are currently holding
    GameObject heldObject = null;

    float speed = 5.0f;
    float jumpForce = 400.0f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
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

        if (playerInput.actions["Interact"].WasPressedThisFrame() && hoveredObject != null) {
            PlayerInteract();
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

    // Allows player to pickup objects
    void PlayerInteract() {
        if (heldObject == null) {
            heldObject = hoveredObject;
            hoveredObject = null;
        } else {
            heldObject.transform.SetParent(gameObject.transform.parent);
            // Swap the held object and object on ground
            heldObject.transform.position = hoveredObject.transform.position;
            var tempObject = heldObject;
            heldObject = hoveredObject;
            hoveredObject = tempObject;
        }
        heldObject.transform.SetParent(gameObject.transform);
        // Moves item right above player's head
        heldObject.transform.localPosition = Vector2.up;
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

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Item" && col.gameObject!= heldObject) {
            hoveredObject = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Item" && col.gameObject!= heldObject) {
            hoveredObject = null;
        }
    }
}
