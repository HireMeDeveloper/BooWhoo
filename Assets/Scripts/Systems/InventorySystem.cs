using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>InventorySystem</c> class is a singleton that manages the player's inventory in a game.
/// It keeps track of items, allows items to be added and removed, and tracks the currently selected item.
/// </summary>
/// <remarks>
/// Public functions:
/// <list type="bullet">
///     <item>
///         <description><c>AddItem(ItemSO item)</c>: Adds an item to the inventory and updates the selected item.</description>
///     </item>
///     <item>
///         <description><c>RemoveItem(ItemSO item)</c>: Removes an item from the inventory and updates the selected item.</description>
///     </item>
///     <item>
///         <description><c>IncrementSelectedItemIndex()</c>: Moves to the next item in the inventory and updates the selected item.</description>
///     </item>
/// </list>
/// </remarks>
public class InventorySystem : MonoBehaviour
{
	public static InventorySystem Instance { get; private set; }

	public event EventHandler<OnItemListChangedEventArgs> OnItemListChanged;
	public class OnItemListChangedEventArgs : EventArgs
	{
		public List<ItemSO> InventoryList;
	}

	public event EventHandler<OnSelectedItemChangedEventArgs> OnSelectedItemChanged;
	public class OnSelectedItemChangedEventArgs : EventArgs
	{
		public ItemSO Item;
	}

	private int selectedItemIndex = 0;

	public ItemSO SelectedItem { get; private set; }
	public List<ItemSO> InventoryList { get; private set; }

	private void Awake()
	{
		Instance = this;
		InventoryList = new List<ItemSO>();
	}

	public void IncrementSelectedItemIndex()
	{
		if (InventoryList.Count == 0) return; // If there are no items in the inventory, return

		// Increment the selected item index
		selectedItemIndex++;
		if (selectedItemIndex >= InventoryList.Count) selectedItemIndex = 0;

		// Update the selected item
		SelectedItem = InventoryList[selectedItemIndex];
		OnSelectedItemChanged?.Invoke(this, new OnSelectedItemChangedEventArgs
		{
			Item = SelectedItem
		});
	}

	private void ResetSelectedItemIndex()
	{
		selectedItemIndex = InventoryList.Count - 1; // Set to the last item

		if (selectedItemIndex < 0) // If there are no items in the inventory
			SelectedItem = null;
		else
			SelectedItem = InventoryList[selectedItemIndex];

		OnSelectedItemChanged?.Invoke(this, new OnSelectedItemChangedEventArgs
		{
			Item = SelectedItem
		});
	}

	public void AddItem(ItemSO item)
	{
		if (InventoryList.Contains(item)) return;

		InventoryList.Add(item);

		ResetSelectedItemIndex();

		OnItemListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			InventoryList = InventoryList
		});
	}

	public void RemoveItem(ItemSO item)
	{
		if (!InventoryList.Contains(item)) return;

		InventoryList.Remove(item);

		ResetSelectedItemIndex();

		OnItemListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			InventoryList = InventoryList
		});
	}
}
