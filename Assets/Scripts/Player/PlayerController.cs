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
    // List of interactables colliding with player.
    // Remove when no longer colliding.
    List<GameObject> hoveredInteractables = new List<GameObject>();

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

        if (playerInput.actions["Interact"].WasPressedThisFrame() && hoveredInteractables.Count > 0) {
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
        if (hoveredInteractables[0].tag == "Item") {
            var pickedItem = hoveredInteractables[0];
            hoveredInteractables.RemoveAt(0);
            // TODO: Add to inventory via global inventory manager
            Destroy(pickedItem);
        } else if (hoveredInteractables[0].tag == "NPC") {
            Debug.Log("We talked to an NPC");
            // TODO: Trigger NPC dialogue code
        } else if (hoveredInteractables[0].tag == "Searchable") {
            var pickedItem = hoveredInteractables[0];
            pickedItem.GetComponent<InteractableItem>().PlayOpenAnim();
            hoveredInteractables.RemoveAt(0);
        }

        // NOTE: Old item code, keeping in case we want to change it back later
        // Allows player to carry one item max.

        /*if (heldObject == null) {
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
        heldObject.transform.localPosition = Vector2.up;*/
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
        if (col.gameObject.tag == "Item" || col.gameObject.tag == "NPC" || col.gameObject.tag == "Searchable") {
            // Gives last collided object priority for interaction if objects overlap
            hoveredInteractables.Insert(0, col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Item" || col.gameObject.tag == "NPC" || col.gameObject.tag == "Searchable") {
            for (int i = 0; i < hoveredInteractables.Count; i++) {
                if (col.gameObject == hoveredInteractables[i]) {
                    hoveredInteractables.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
