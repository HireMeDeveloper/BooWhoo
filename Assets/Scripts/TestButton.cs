using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
	[SerializeField] private TaskSO[] taskSOs;
	[SerializeField] private ItemSO[] itemSOs;

	public Button addTask;
	public Button completeTask;
	public Button addItem;
	public Button changeItem;

	public Button addCandy;
	public Button removeCandy;

	int addIndex = 0;

	int addItemIndex = 0;
	private void Awake()
	{
		if (addTask == null)
		{
			Debug.LogError("Buttons not assigned");
			return;
		}
		addTask.onClick.AddListener(() =>
		{
			AddTask();
		});


		completeTask.onClick.AddListener(() =>
		{
			TaskComplete();
		});

		addItem.onClick.AddListener(() =>
		{

			AddItem();
		});

		changeItem.onClick.AddListener(() =>
		{
			InventorySystem.Instance.IncrementSelectedItemIndex();
		});

		addCandy.onClick.AddListener(() =>
		{
			CandyInventory.Instance.AddCandy(1);
		});

		removeCandy.onClick.AddListener(() =>
		{
			CandyInventory.Instance.RemoveCandy(1);
		});
	}



	/// <summary>
	/// Replicates how when the player interacts with an item, it gets added to the inventory
	/// </summary>
	private void AddItem()
	{
		if (addItemIndex >= itemSOs.Length) return;

		InventorySystem.Instance.AddItem(itemSOs[addItemIndex]); // Add the item to the inventory
		addItemIndex++;

	}

	/// <summary>
	/// Replicates how the NPC will give the player a task
	/// </summary>
	private void AddTask()
	{
		TaskSystem.Instance.AddTask(taskSOs[addIndex]); // Add the task

		addIndex++;
		if (addIndex >= taskSOs.Length)
		{
			addIndex = 0;
		}
	}

	/// <summary>
	/// Replicates what the NPC does when the player gives the required item for its quest
	/// </summary>
	private void TaskComplete()
	{
		ItemSO item = InventorySystem.Instance.SelectedItem;  // Gets the Inventory system's selected item
		foreach (var task in taskSOs)
		{
			if (task.requiredItem == item) // Check if the task's required item is the same as the selected item
			{
				TaskSystem.Instance.FinishTask(task); // Finish the task 
				break;
			}
		}

	}
}
