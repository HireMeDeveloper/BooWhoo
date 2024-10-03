using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
	[SerializeField] private ItemSO[] itemSO;
	public Button addItem;
	public Button removeItem;
	public Button completeTask;

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
			InventorySystem.Instance.UpdateSelectedItemIndex();
		});

		completeTask.onClick.AddListener(() =>
		{
			QuestComplete();
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

	private void QuestComplete()
	{

		itemSO[2].QuestDone();
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
