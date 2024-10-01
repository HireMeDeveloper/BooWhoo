using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
	public static InventorySystem Instance { get; private set; }
	public event EventHandler<OnItemListChangedEventArgs> OnItemListChanged;
	public class OnItemListChangedEventArgs : EventArgs
	{
		public List<ItemSO> Items;
		public ItemSO ItemChanged;
		public bool IsItemRemoved;
	}

	public event EventHandler<OnSelectedItemChangedEventArgs> OnSelectedItemChanged;
	public class OnSelectedItemChangedEventArgs : EventArgs
	{
		public ItemSO Item;
	}

	[SerializeField] private int maxInventory = 15;
	private List<ItemSO> items;
	private ItemSO selectedItem;

	private void Awake()
	{
		Instance = this;
		items = new List<ItemSO>();
	}

	private void Start()
	{
		SetSelectedItem(null);
	}

	public void SetSelectedItem(ItemSO itemSO)
	{
		selectedItem = itemSO;
		OnSelectedItemChanged?.Invoke(this, new OnSelectedItemChangedEventArgs
		{
			Item = selectedItem
		});
	}

	public void AddItem(ItemSO item)
	{
		if (items.Count >= maxInventory) return;

		items.Add(item);
		OnItemListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			Items = items,
			ItemChanged = item,
			IsItemRemoved = false
		});
	}

	public void RemoveItem(ItemSO item)
	{
		if (!items.Contains(item)) return;

		if (selectedItem != null && item == selectedItem) // If the item is selected, deselect it
		{
			SetSelectedItem(null);
		}

		items.Remove(item);
		OnItemListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			Items = items,
			ItemChanged = item,
			IsItemRemoved = true
		});
	}
}
