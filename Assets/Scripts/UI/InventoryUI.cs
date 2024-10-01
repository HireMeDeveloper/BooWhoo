using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

	[SerializeField] private Transform inventorySlotContent;
	[SerializeField] private Transform selectedItemContent;
	[SerializeField] private TextMeshProUGUI selectedItemName;
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
		InventorySystem.Instance.OnSelectedItemChanged += Inventory_OnSelectedItemChanged;
	}

	private void Inventory_OnSelectedItemChanged(object sender, InventorySystem.OnSelectedItemChangedEventArgs e)
	{
		if (e.Item == null) { selectedItemName.text = ""; return; }
		selectedItemName.text = e.Item.Name;
	}

	private void Inventory_OnItemListChanged(object sender, InventorySystem.OnItemListChangedEventArgs e)
	{

		RefreshList(e.Items);

		if (e.IsItemRemoved) // Item Removed
		{

		}
		else // Item Added
		{

		}
	}

	private void RefreshList(List<ItemSO> itemSOs)
	{
		ClearItemList();

		int index = 0;
		foreach (var item in itemSOs)
		{
			itemList[index].SetItemData(item);
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
