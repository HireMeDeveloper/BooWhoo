using System;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public class Conversation
{
    public List<ConversationLine> conversationLines;
    public UnityEvent onConversationEnd;
    private int currentLine = -1;

    public void Init()
    {
        currentLine = -1;
    }

    public string GetNextLine()
    {
        if (currentLine == -1)
        {
            currentLine = 0;
        }
        else
        {
            currentLine++;
        }

        if (currentLine >= conversationLines.Count)
        {
            EndConversation();
            return null;
        }

        return conversationLines[currentLine].text;
    }

    public void TriggerLineEnd()
    {
        conversationLines[currentLine].TriggerLineEnd();
    }

    public void EndConversation()
    {
        if (onConversationEnd != null)
        {
            onConversationEnd.Invoke();
        }
    }
}
