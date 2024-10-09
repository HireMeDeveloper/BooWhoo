using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>TaskSystem</c> class manages a list of tasks in a game environment.
/// It is a singleton that keeps track of the tasks, allows tasks to be added,
/// and updates the task list when tasks are completed.
/// </summary>
/// <remarks>
/// Public functions:
/// <list type="bullet">
///     <item>
///         <description><c>AddTask(TaskSO item)</c>: Adds a task to the task list and triggers the task list changed event.</description>
///     </item>
///     <item>
///         <description><c>FinishTask(TaskSO task)</c>: Marks the task as completed, removes it from the inventory, and moves it to the end of the list.</description>
///     </item>
/// </list>
/// </remarks>
public class TaskSystem : MonoBehaviour
{
	public static TaskSystem Instance { get; private set; }
	public event EventHandler<OnItemListChangedEventArgs> OnTaskListChanged;
	public class OnItemListChangedEventArgs : EventArgs
	{
		public List<TaskSO> TaskList;
	}


	private List<TaskSO> taskList;


	private void Awake()
	{
		Instance = this;
		taskList = new List<TaskSO>();
	}

	private void Start()
	{

		OnTaskListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			TaskList = taskList
		});
	}



	public void AddTask(TaskSO item)
	{
		if (taskList.Contains(item)) return;

		int firstIndex = 0;
		taskList.Insert(firstIndex, item); // Put it at the start of the list

		OnTaskListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			TaskList = taskList
		});

	}


	public void FinishTask(TaskSO task)
	{
		// Check if the task is in the list and if it is done
		if (!taskList.Contains(task) || task.IsDone) return;

		// Mark as done
		task.IsDone = true;

		// Remove
		taskList.Remove(task);

		// Remove the required item of the task in the inventory
		InventorySystem.Instance.RemoveItem(task.requiredItem);


		// Insert at the end
		int lastIndex = taskList.Count;
		taskList.Insert(lastIndex, task); // Put it at the end of the list

		OnTaskListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			TaskList = taskList
		});
	}

}
