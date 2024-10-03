using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
	public ItemSO ItemData { get; private set; }
	// [SerializeField] private Image itemImage;
	// [SerializeField] private TextMeshProUGUI itemName;
	[SerializeField] private TextMeshProUGUI itemDescription;
	// [SerializeField] private Button button;


	private void Awake()
	{
	}
	private void Start()
	{
		SetItemData(ItemData);
	}

	public void SetItemData(ItemSO item)
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
