using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] PlayerDialogUser playerDialogUser;
    // List of interactables colliding with player.
    // Remove from list when no longer colliding in InteractableItems.cs
    public List<GameObject> hoveredInteractables = new List<GameObject>();
    

    // Allows player to interact with objects and NPCs
    public void Interact() {
        if (hoveredInteractables[0].tag == "Item" || hoveredInteractables[0].tag == "Searchable") {
            AudioManager.CreateAudio("Interact");
            var pickedItem = hoveredInteractables[0];
            pickedItem.GetComponent<InteractableItem>().OnInteract(this);

            if (pickedItem.tag == "Item") {
                playerDialogUser.FoundItem();
            } else if (pickedItem.tag == "Searchable" && pickedItem.GetComponent<InteractableItem>().isEmpty) {
                playerDialogUser.OpenedEmpty();

            } else if (pickedItem.tag == "Searchable" && !pickedItem.GetComponent<InteractableItem>().isEmpty) {
                playerDialogUser.OpenedNotEmpty();
            }
            // TODO: Add to inventory via global inventory manager
        } else if (hoveredInteractables[0].tag == "NPC") {
            var current_npc = hoveredInteractables[0];
            current_npc.GetComponent<NPCDialogUser>().OnInteract(this);
        }
    }
}
