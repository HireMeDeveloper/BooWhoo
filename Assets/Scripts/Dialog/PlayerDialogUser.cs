using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogUser : DialogUser
{
    [SerializeField] private Conversation emptyConversation;
    [SerializeField] private Conversation notEmptyConversation;
    [SerializeField] private Conversation foundItemConversation;

    public void OpenedEmpty() {
        TriggerConversation(emptyConversation);
    }

    public void OpenedNotEmpty() {
        TriggerConversation(notEmptyConversation);
    }

    public void FoundItem() {
        TriggerConversation(foundItemConversation);
    }
}
