using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskSystemUI : MonoBehaviour
{
	[SerializeField] private Transform content;

	private Task[] taskList;

	private void Start()
	{
		if (content.childCount <= 0)
		{
			Debug.LogError("No inventory slots found");
			return;
		}

		// Get the UI task lists from the inventory slot content
		taskList = new Task[content.childCount];
		for (int i = 0; i < content.childCount; i++)
		{
			taskList[i] = content.GetChild(i).GetComponent<Task>();
		}

		TaskSystem.Instance.OnTaskListChanged += (object sender, TaskSystem.OnItemListChangedEventArgs e) =>
		{
			RefreshList(e.TaskList);
		};
	}

	/// <summary>
	/// Refresh the UI list
	/// </summary>
	/// <param name="TaskSOs"></param>
	private void RefreshList(List<TaskData> TaskSOs)
	{
		ClearItemList(); // Clear the UI list

		int index = 0;
		foreach (var task in TaskSOs) // Set item data to the UI
		{
			TaskSystem.Instance.GetTaskSO(task, out TaskSO taskSO);
			taskList[index].SetItemData(taskSO);
			TextMeshProUGUI itemText = taskList[index].GetComponentInChildren<TextMeshProUGUI>();
			if (task.IsDone)
			{
				string originalText = itemText.text;
				// Add strike through effect
				itemText.text = $"<s>{originalText}</s>";
				// Change font color to gray
				itemText.color = Color.gray;
			}
			else
			{
				itemText.color = Color.white; // Default white
			}
			index++;
		}

	}

	/// <summary>
	/// Clear the UI list
	/// </summary>
	private void ClearItemList()
	{
		if (taskList.Length <= 0) return; // If the list is empty, return

		foreach (var item in taskList)
		{
			item.SetItemData(null); // Set to nothing
		}
	}
}
