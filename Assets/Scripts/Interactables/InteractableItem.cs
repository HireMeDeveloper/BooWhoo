using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public Animator anim;
    bool opened = false;

    public void PlayOpenAnim() {
        if (!opened) {
            anim.Play("Open");
            opened = true;
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
