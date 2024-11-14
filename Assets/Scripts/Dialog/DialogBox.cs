using TMPro;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogTextComponent;
    [SerializeField] private GameObject dialogGameObject;

    public void Show()
    {
        dialogGameObject.SetActive(true);
    }

    public void Hide()
    {
        dialogGameObject.SetActive(false);
    }
    public void DisplayDialog(string text, int visibleChars)
    {
        if (string.IsNullOrEmpty(text))
        {
            Hide();
            return;
        }
        else
        {
            Show();
        }

        dialogTextComponent.text = text;
        dialogTextComponent.maxVisibleCharacters = visibleChars;
    }
}
