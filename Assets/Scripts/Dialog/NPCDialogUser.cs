using UnityEngine;
public class NPCDialogUser : DialogUser
{
    [SerializeField] private Conversation preHelpConversation;
    [SerializeField] private Conversation postHelpConversation;
    [SerializeField] private Conversation giveCandyConversation;
    [SerializeField] private Conversation giveItemConversation;

    private bool hasBeenHelped = false;
    private bool hasBeenGivenCandy = false;

    private void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.L))
        {
            Listen();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            GiveCandy();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GiveItem();
        }
    }

    public void Interact()
    {
        // TODO: Read candy count from player prefs
        var candyCount = 0;

        if (hasBeenGivenCandy == false && candyCount > 0)
        {
            GiveCandy();
            return;
        }

        // TODO: Read currentItem from player prefs
        var hasValidItem = false;

        if (hasValidItem)
        {
            GiveItem();
            return;
        }

        Listen();
    }

    private void GiveCandy()
    {
        hasBeenGivenCandy = true;

        // TODO: Update the candy counts in player prefs

        TriggerConversation(giveCandyConversation);
    }

    private void Listen()
    {
        if (hasBeenHelped == false)
        {
            TriggerConversation(preHelpConversation);
        }
        else
        {
            TriggerConversation(postHelpConversation);
        }
    }

    private void GiveItem()
    {
        hasBeenHelped = true;
        TriggerConversation(giveItemConversation);
    }
}
