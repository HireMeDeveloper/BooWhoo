using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
	[SerializeField] private ItemSO[] itemSO;
	public Button addItem;
	public Button removeItem;

	int index = 0;
	private void Awake()
	{
		if (addItem == null || removeItem == null)
		{
			Debug.LogError("Buttons not assigned");
			return;
		}
		addItem.onClick.AddListener(() =>
		{
			AddItem();
		});
		removeItem.onClick.AddListener(() =>
		{
			RemoveItem();
		});
	}

	private void AddItem()
	{
		InventorySystem.Instance.AddItem(itemSO[index]);
		index++;
		if (index >= itemSO.Length)
		{
			index = 0;
		}
	}

	private void RemoveItem()
	{
		InventorySystem.Instance.RemoveItem(itemSO[index]);
		index++;
		if (index >= itemSO.Length)
		{
			index = 0;
		}
	}
}
