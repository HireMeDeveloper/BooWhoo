using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogUser : DialogUser
{
    [SerializeField] private Conversation emptyConversation;
    [SerializeField] private Conversation notEmptyConversation;
    [SerializeField] private Conversation foundItemConversation;

    private float timer = 0.0f;
    private float maxTime = 3.0f;
    void Update() {
        base.Update();
        if (isTalking) {
            timer += Time.deltaTime;
            if (timer > maxTime) {
                timer = 0.0f;
                CloseConversation();
            }
        }
    }

    public void OpenedEmpty() {
        TriggerConversation(emptyConversation);
        // Need to reset timer or the dialog might close before player can read if 
        // player interacts with different object at 2.5 seconds for example
        timer = 0.0f; 
    }

    public void OpenedNotEmpty() {
        TriggerConversation(notEmptyConversation);
        timer = 0.0f;
    }

    public void FoundItem() {
        TriggerConversation(foundItemConversation);
        timer = 0.0f;
    }
}
