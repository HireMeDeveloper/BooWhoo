using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public Animator anim;
    // Check this in editor for empty searchables
    public bool isEmpty;
    public bool opened = false;

    public void PlayOpenAnim() {
        if (!opened) {
            anim.Play("Open");
            opened = true;
        }
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            OnEnter(col.gameObject.GetComponent<PlayerInteraction>());
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            OnExit(col.gameObject.GetComponent<PlayerInteraction>());
        }
    }

    
    // IInteractable methods
    public void OnEnter(PlayerInteraction playerInteraction) {
        playerInteraction.hoveredInteractables.Insert(0, gameObject);
        if (!opened) {
            anim.Play("Wiggle");
        }
    }
    public void OnExit(PlayerInteraction playerInteraction) {
        for (int i = 0; i < playerInteraction.hoveredInteractables.Count; i++) {
            if (gameObject == playerInteraction.hoveredInteractables[i]) {
                playerInteraction.hoveredInteractables.RemoveAt(i);
                break;
            }
        }
    }
    public void OnInteract(PlayerInteraction playerInteraction) {
        playerInteraction.hoveredInteractables.RemoveAt(0);
        if (gameObject.tag == "Item") {
            Destroy(gameObject);
        } else if (gameObject.tag == "Searchable") {
            PlayOpenAnim();
        }
    }
}
