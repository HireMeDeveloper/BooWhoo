using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task : MonoBehaviour
{
    public TaskSO ItemData { get; private set; }

    [SerializeField] private TextMeshProUGUI itemDescription;



    private void Awake()
    {
    }
    private void Start()
    {
        SetItemData(ItemData);
    }

    public void SetItemData(TaskSO item)
    {
        ItemData = item;
        if (item != null)
        {
            // itemImage.sprite = itemData.SpriteIcon;
            // itemName.text = itemData.Name;
            // button.interactable = true;
            itemDescription.text = ItemData.Description;
        }
        else
        {
            itemDescription.text = "";
            // button.interactable = false;

        }
    }
}
