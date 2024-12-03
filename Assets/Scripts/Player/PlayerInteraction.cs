using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerDialogUser playerDialogUser;
    // List of interactables colliding with player.
    // Remove from list when no longer colliding in InteractableItems.cs
    public List<GameObject> hoveredInteractables = new List<GameObject>();
    void Start() {
        playerDialogUser = GetComponent<PlayerDialogUser>();
    }

    // Allows player to interact with objects and NPCs
    public void Interact() {
        if (hoveredInteractables[0].tag == "Item" || hoveredInteractables[0].tag == "Searchable") {
            var pickedItem = hoveredInteractables[0];
            var item = pickedItem.GetComponent<InteractableItem>();
            
            if (pickedItem.tag == "Item") {
                playerDialogUser.FoundItem();
                AudioManager.CreateAudio("Interact");
            } else if (pickedItem.tag == "Searchable" && !item.opened && item.isEmpty) {
                playerDialogUser.OpenedEmpty();
                // If CreateAudio isn't in if statements, the audio will still play on already opened containers.
                AudioManager.CreateAudio("Interact");
            } else if (pickedItem.tag == "Searchable" && !item.opened && !item.isEmpty) {
                playerDialogUser.OpenedNotEmpty();
                AudioManager.CreateAudio("Interact");
            }
            item.OnInteract(this);
            // TODO: Add to inventory via global inventory manager
        } else if (hoveredInteractables[0].tag == "NPC") {
            var current_npc = hoveredInteractables[0];
            current_npc.GetComponent<NPCDialogUser>().OnInteract(this);
        }
    }
}
