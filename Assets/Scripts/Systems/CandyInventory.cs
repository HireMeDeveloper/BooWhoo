using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyInventory
{
	public static CandyInventory Instance { get; private set; }
	/// <summary>
	///	The PlayerPrefs key to store the amount of candy in the player's inventory.
	/// </summary>
	public static string CANDY_AMOUNT_KEY = "Candy_Inventory_Amount";

	/// <summary>
	/// When the amount of candy in the player's inventory has changed.
	/// </summary>
	public static event EventHandler<OnCandyAmountChangedEventArgs> OnCandyAmountChanged;
	public class OnCandyAmountChangedEventArgs : EventArgs
	{
		public int CandyAmount;
	}

	/// <summary>
	/// The amount of candy in the player's inventory.
	/// </summary>
	public int CandyCount { get; private set; }
	public CandyInventory(bool isClearPlayerPref = false)
	{
		Instance = this;
		if (isClearPlayerPref)
		{
			PlayerPrefs.DeleteKey(CANDY_AMOUNT_KEY);
			Debug.Log($"Cleared PlayerPrefs {CANDY_AMOUNT_KEY}");
		}
		CandyCount = LoadFromPlayerPref();
	}

	public void OnDisable()
	{
		SaveToPlayerPref();
	}

	/// <summary>
	/// Adds candy to the player's inventory.
	/// </summary>
	/// <param name="amount"></param>
	public void AddCandy(int amount)
	{
		CandyCount += amount;
		OnCandyAmountChanged?.Invoke(this, new OnCandyAmountChangedEventArgs { CandyAmount = CandyCount });
	}

	/// <summary>
	/// Removes candy from the player's inventory.
	/// </summary>
	/// <param name="amount"></param>
	public void RemoveCandy(int amount)
	{
		CandyCount -= amount;
		if (CandyCount <= 0)
		{
			CandyCount = 0;
		}
		OnCandyAmountChanged?.Invoke(this, new OnCandyAmountChangedEventArgs { CandyAmount = CandyCount });
	}

	/// <summary>
	/// Saves the amount of candy in the player's inventory to PlayerPrefs.
	/// </summary>
	private void SaveToPlayerPref()
	{
		// Serialize the taskList to JSON
		string json = JsonUtility.ToJson(new CandyInventoryWrapper { CandyCount = this.CandyCount });
		PlayerPrefs.SetString(CANDY_AMOUNT_KEY, json);
		PlayerPrefs.Save(); // Ensure PlayerPrefs is saved
	}

	/// <summary>
	/// Load the amount of candy in the player's inventory from PlayerPrefs.
	/// </summary>
	/// <returns></returns>
	private int LoadFromPlayerPref()
	{
		int candyCount = 0;
		if (PlayerPrefs.HasKey(CANDY_AMOUNT_KEY)) // If the key exists in PlayerPrefs
		{
			string json = PlayerPrefs.GetString(CANDY_AMOUNT_KEY);
			CandyInventoryWrapper wrapper = JsonUtility.FromJson<CandyInventoryWrapper>(json);
			candyCount = wrapper.CandyCount;
		}
		else // If the key does not exist in PlayerPrefs, Save an empty one to PlayerPrefs
			SaveToPlayerPref();


		return candyCount;
	}

	[Serializable]
	private class CandyInventoryWrapper
	{
		public int CandyCount;
	}
}

