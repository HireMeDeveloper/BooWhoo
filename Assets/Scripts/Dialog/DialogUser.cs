using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DialogUser : MonoBehaviour
{
    [SerializeField] private DialogBox dialogBox;

    private Conversation currentConversation;

    private string currentDialog = "";
    private int currentDialogIndex = 0;

    private float typeDelay = 0.025f;
    private bool isTyping = false;

    public UnityEvent OnTypeChar;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    protected void Update()
    {
        if (playerControls.player.dialogoptionnext.WasPerformedThisFrame())
        {
            if (isTyping)
            {
                SkipDialog();
            }
            else
            {
                MoveToNextLine();
            }
        }
    }

    public void TriggerConversation(Conversation conversation, Action callback)
    {
        currentConversation = conversation;
        currentConversation.Init();

        if (callback != null) currentConversation.onConversationEnd.AddListener(() => callback.Invoke());

        var firstLine = conversation.GetNextLine();
        StartTyping(firstLine);
    }

    public void TriggerConversation(Conversation conversation)
    {
        TriggerConversation(conversation, null);
    }

    public void CloseConversation() {
        StopTyping();
        dialogBox.Hide();
    }

    private void MoveToNextLine()
    {
        if (currentConversation == null) return;

        var nextLine = currentConversation.GetNextLine();
        if (nextLine == null)
        {
            dialogBox.Hide();
            return;
        }

        StartTyping(nextLine);
    }

    private void StartTyping(string dialog)
    {
        StopTyping();

        currentDialog = dialog;
        currentDialogIndex = 0;

        StartCoroutine(TypingTimer());
    }

    private void StopTyping()
    {
        StopAllCoroutines();
        isTyping = false;
    }

    private void SkipDialog()
    {
        StopTyping();
        currentDialogIndex = currentDialog.Length - 1;

        UpdateDialog();
    }

    private IEnumerator TypingTimer()
    {
        isTyping = true;
        var dialogLength = currentDialog.Length;

        while (currentDialogIndex < currentDialog.Length)
        {
            OnTypeChar.Invoke();

            currentDialogIndex++;

            UpdateDialog();

            yield return new WaitForSeconds(typeDelay);
        }

        isTyping = false;
        currentConversation.TriggerLineEnd();
    }

    private void UpdateDialog()
    {
        dialogBox.DisplayDialog(currentDialog, currentDialogIndex);
    }
}
