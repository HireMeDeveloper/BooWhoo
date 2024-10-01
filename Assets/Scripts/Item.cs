using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
	[SerializeField] private ItemSO itemData;
	[SerializeField] private Image backgroundImage;
	[SerializeField] private TextMeshProUGUI itemName;
	[SerializeField] private Button button;


	private void Awake()
	{
		button.onClick.AddListener(() => { InventorySystem.Instance.SetSelectedItem(itemData); });
	}
	private void Start()
	{
		SetItemData(itemData);
	}

	public void SetItemData(ItemSO item)
	{
		itemData = item;
		if (item != null)
		{
			backgroundImage.sprite = itemData.SpriteIcon;
			itemName.text = itemData.Name;
			button.interactable = true;
		}
		else
		{
			itemName.text = "";
			button.interactable = false;
		}
	}

}
