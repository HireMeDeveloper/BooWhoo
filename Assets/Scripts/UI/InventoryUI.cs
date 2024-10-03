using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

	[SerializeField] private Transform inventorySlotContent;
	[SerializeField] private Transform itemUIPrefab;

	private Item[] itemList;

	private void Start()
	{
		if (inventorySlotContent.childCount <= 0)
		{
			Debug.LogError("No inventory slots found");
			return;
		}

		itemList = new Item[inventorySlotContent.childCount];
		for (int i = 0; i < inventorySlotContent.childCount; i++)
		{
			itemList[i] = inventorySlotContent.GetChild(i).GetComponent<Item>();
		}

		InventorySystem.Instance.OnItemListChanged += Inventory_OnItemListChanged;
	}



	private void Inventory_OnItemListChanged(object sender, InventorySystem.OnItemListChangedEventArgs e)
	{

		RefreshList(e.Items);
	}

	private bool GetItemUI(ItemSO itemSO, out Item retItem)
	{
		retItem = null;
		foreach (var item in itemList)
		{
			if (item.ItemData == itemSO)
			{
				retItem = item;
				return true;
			}
		}

		return false;
	}

	private void RefreshList(List<ItemSO> itemSOs)
	{
		ClearItemList(); // Clear the UI list

		int index = 0;
		foreach (var item in itemSOs) // Set item data to the UI
		{
			itemList[index].SetItemData(item);
			TextMeshProUGUI itemText = itemList[index].GetComponentInChildren<TextMeshProUGUI>();
			if (item.IsQuestDone)
			{

				string originalText = itemText.text;

				// Add strike through effect
				itemText.text = $"<s>{originalText}</s>";
				// Change font color to gray
				itemText.color = Color.gray;
			}
			else
			{
				itemText.color = Color.white;
			}
			index++;
		}
	}

	private void ClearItemList()
	{
		if (itemList.Length <= 0) return;
		foreach (var item in itemList)
		{
			item.SetItemData(null);
		}
	}
}
