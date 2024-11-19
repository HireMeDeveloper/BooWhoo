using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyInventory
{
	public static CandyInventory Instance { get; private set; }
	public static string CANDY_AMOUNT_KEY = "Candy_Inventory_Amount";

	public static event EventHandler<OnCandyAmountChangedEventArgs> OnCandyAmountChanged;
	public class OnCandyAmountChangedEventArgs : EventArgs
	{
		public int CandyAmount;
	}

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

	public void AddCandy(int amount)
	{
		CandyCount += amount;
		OnCandyAmountChanged?.Invoke(this, new OnCandyAmountChangedEventArgs { CandyAmount = CandyCount });
	}

	public void RemoveCandy(int amount)
	{
		CandyCount -= amount;
		if (CandyCount <= 0)
		{
			CandyCount = 0;
		}
		OnCandyAmountChanged?.Invoke(this, new OnCandyAmountChangedEventArgs { CandyAmount = CandyCount });
	}

	private void SaveToPlayerPref()
	{
		// Serialize the taskList to JSON
		string json = JsonUtility.ToJson(new CandyInventoryWrapper { CandyCount = this.CandyCount });
		PlayerPrefs.SetString(CANDY_AMOUNT_KEY, json);
		PlayerPrefs.Save(); // Ensure PlayerPrefs is saved
	}

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

