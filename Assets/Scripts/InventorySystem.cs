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

	[SerializeField] private int maxInventory = 15;
	private List<ItemSO> items;

	private void Awake()
	{
		Instance = this;
		items = new List<ItemSO>();
	}

	private void Start()
	{

		OnItemListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			Items = items,
			ItemChanged = null,
			IsItemRemoved = false
		});
	}


	public void AddItem(ItemSO item)
	{
		if (items.Count >= maxInventory || items.Contains(item)) return;

		int firstIndex = 0;
		items.Insert(firstIndex, item); // Put it at the start of the list

		OnItemListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			Items = items,
			ItemChanged = item,
			IsItemRemoved = false
		});

		item.OnQuestDone += (sender, e) => // Remove the item when the quest is done
		{
			RemoveItem(item);
		};

	}

	// TODO: Convert to private after implementation of Quest Characters
	public void RemoveItem(ItemSO item)
	{
		if (!items.Contains(item)) return;

		// Remove
		items.Remove(item);

		// Insert at the end
		int lastIndex = items.Count;
		items.Insert(lastIndex, item); // Put it at the end of the list

		OnItemListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			Items = items,
			ItemChanged = item,
			IsItemRemoved = true
		});
	}

	private void OnDestroy()
	{
		foreach (var item in items) // Reset all items
		{
			item.IsQuestDone = false;
		}
	}
}
