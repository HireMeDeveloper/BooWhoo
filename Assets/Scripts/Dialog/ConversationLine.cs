using System;
using UnityEngine.Events;

[Serializable]
public class ConversationLine
{
    public string text;
    public UnityEvent OnLineFinish;

    public void TriggerLineEnd()
    {
        OnLineFinish.Invoke();
    }
}
