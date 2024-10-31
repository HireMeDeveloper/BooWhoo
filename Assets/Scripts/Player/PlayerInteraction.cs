using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // List of interactables colliding with player.
    // Remove from list when no longer colliding.
    public List<GameObject> hoveredInteractables = new List<GameObject>();
    

    // Allows player to interact with objects and NPCs
    public void Interact() {
        AudioManager.CreateAudio("Interact");
        if (hoveredInteractables[0].tag == "Item" || hoveredInteractables[0].tag == "Searchable") {
            var pickedItem = hoveredInteractables[0];
            pickedItem.GetComponent<InteractableItem>().OnInteract(this);
            // TODO: Add to inventory via global inventory manager
        } else if (hoveredInteractables[0].tag == "NPC") {
            Debug.Log("We talked to an NPC");
            // TODO: Trigger NPC dialogue code
        }
    }
}
