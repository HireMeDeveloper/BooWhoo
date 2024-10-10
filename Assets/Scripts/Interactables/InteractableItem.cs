using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public Animator anim;
    // Check this in editor for empty searchables
    public bool isEmpty;
    bool opened = false;

    public void PlayOpenAnim() {
        if (!opened) {
            anim.Play("Open");
            opened = true;
            if (isEmpty) {
                // TODO: Display dialogue
                Debug.Log("It was empty...");
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            if (!opened) {
                Debug.Log("triggered");
                anim.Play("Wiggle");
            }
        }
    }
}
