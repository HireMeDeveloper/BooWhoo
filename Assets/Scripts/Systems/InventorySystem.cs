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
	private const string INVENTORY_LIST_KEY = "InventoryList";
	/// <summary>
	/// When an item has been added or removed from the inventory
	/// </summary>
	public event EventHandler<OnItemListChangedEventArgs> OnItemListChanged;
	public class OnItemListChangedEventArgs : EventArgs
	{
		public List<ItemSO> InventoryList;
	}

	/// <summary>
	/// When the player has selected a new item from the list of items (InventoryList)
	/// </summary>
	public event EventHandler<OnSelectedItemChangedEventArgs> OnSelectedItemChanged;
	public class OnSelectedItemChangedEventArgs : EventArgs
	{
		public ItemSO Item;
	}

	private int selectedItemIndex = 0;

	public ItemSO SelectedItem { get; private set; }
	public List<ItemSO> InventoryList { get; private set; }

	public bool ClearPlayerPref_InventoryList = false;

	private void Awake()
	{
		Instance = this;
		if (ClearPlayerPref_InventoryList) // For debugging, Remove when final build is done
		{
			InventoryList = new();
			SaveTaskListToPlayerPref(InventoryList);
			ClearPlayerPref_InventoryList = false;
			Debug.Log($"Cleared PlayerPrefs {INVENTORY_LIST_KEY} @ Inventory System Game Object: {gameObject.name}");
		}

		InventoryList = LoadInventoryFromPlayerPrefs();


	}

	private void Start()
	{
		ResetSelectedItemIndex();
	}

	public void IncrementSelectedItemIndex()
	{
		if (InventoryList == null || InventoryList.Count == 0) return; // If there are no items in the inventory, return

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

	/// <summary>
	/// Resets the selected item index to the last item in the inventory.
	/// </summary>
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

	/// <summary>
	/// Adds an item to the inventory and updates the selected item.
	/// </summary>
	/// <param name="item"></param>
	public void AddItem(ItemSO item)
	{
		if (InventoryList == null || InventoryList.Contains(item)) return;

		InventoryList.Add(item);

		ResetSelectedItemIndex();

		SaveTaskListToPlayerPref(InventoryList);

		OnItemListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			InventoryList = InventoryList
		});
	}

	/// <summary>
	/// Removes an item from the inventory and updates the selected item.
	/// </summary>
	/// <param name="item"></param>
	public void RemoveItem(ItemSO item)
	{
		if (InventoryList == null || !InventoryList.Contains(item)) return;

		InventoryList.Remove(item);

		ResetSelectedItemIndex();

		SaveTaskListToPlayerPref(InventoryList);
		LoadInventoryFromPlayerPrefs();

		OnItemListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			InventoryList = InventoryList
		});
	}

	/// <summary>
	/// Saves the task list to PlayerPrefs.
	/// </summary>
	/// <param name="_InventoryList"></param>
	private void SaveTaskListToPlayerPref(List<ItemSO> _InventoryList)
	{
		// Serialize the taskList to JSON
		string json = JsonUtility.ToJson(new InventoryListWrapper { InventoryList = _InventoryList });
		PlayerPrefs.SetString(INVENTORY_LIST_KEY, json);
		PlayerPrefs.Save(); // Ensure PlayerPrefs is saved
	}

	/// <summary>
	/// Load the task list from PlayerPrefs.
	/// </summary>
	/// <returns></returns>
	public List<ItemSO> LoadInventoryFromPlayerPrefs()
	{
		List<ItemSO> _inventoryList = new List<ItemSO>();
		if (PlayerPrefs.HasKey(INVENTORY_LIST_KEY)) // If the key exists in PlayerPrefs
		{
			string json = PlayerPrefs.GetString(INVENTORY_LIST_KEY);
			InventoryListWrapper wrapper = JsonUtility.FromJson<InventoryListWrapper>(json);
			_inventoryList = wrapper.InventoryList;
		}
		else // If the key does not exist in PlayerPrefs, Save an empty one to PlayerPrefs
			SaveTaskListToPlayerPref(_inventoryList);

		OnItemListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			InventoryList = _inventoryList
		});

		return _inventoryList;
	}

	/// <summary>
	/// Wrapper class to serialize the task list to JSON.
	/// </summary>
	[Serializable]
	private class InventoryListWrapper
	{
		public List<ItemSO> InventoryList;
	}
}
