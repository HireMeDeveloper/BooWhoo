using UnityEngine;
public class NPCDialogUser : DialogUser, IInteractable
{
    [SerializeField] private Conversation preHelpConversation;
    [SerializeField] private Conversation postHelpConversation;
    [SerializeField] private Conversation giveCandyConversation;
    [SerializeField] private Conversation giveItemConversation;

    private bool hasBeenHelped = false;
    private bool hasBeenGivenCandy = false;
    private bool playedPreHelpOnce = false;
    

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
        speechBubble.SetActive(true);
        playerInteraction.hoveredInteractables.Insert(0, gameObject);
    }
    public void OnExit(PlayerInteraction playerInteraction) {
        CloseConversation();
        speechBubble.SetActive(false);
        for (int i = 0; i < playerInteraction.hoveredInteractables.Count; i++) {
            if (gameObject == playerInteraction.hoveredInteractables[i]) {
                playerInteraction.hoveredInteractables.RemoveAt(i);
                break;
            }
        }
    }
    public void OnInteract(PlayerInteraction playerInteraction) {
        if (!isTalking) {
            speechBubble.SetActive(false);
            if (!playedPreHelpOnce) {
                Listen();
                playedPreHelpOnce = true;
                return;
            }

            // TODO: Read candy count from player prefs
            var candyCount = 1;

            if (hasBeenGivenCandy == false && candyCount > 0)
            {
                GiveCandy();
                return;
            }

            // TODO: Read currentItem from player prefs
            var hasValidItem = true;

            if (hasBeenHelped == false && hasValidItem)
            {
                GiveItem();
                return;
            }

            Listen();
        }
    }
}
