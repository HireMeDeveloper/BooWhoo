using System;
using System.Collections.Generic;
using System.Linq;
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
	private const string TASK_SO_PATH = "ScriptableObjects/Tasks";
	private const string TASK_LIST_KEY = "TaskList";

	public event EventHandler<OnItemListChangedEventArgs> OnTaskListChanged;
	public class OnItemListChangedEventArgs : EventArgs
	{
		public List<TaskData> TaskList;
	}

	private List<TaskData> taskDataList;
	public HashSet<TaskSO> MasterTaskSOList;

	public bool ClearPlayerPref_TaskList = true;

	private void Awake()
	{
		Instance = this;

		if (ClearPlayerPref_TaskList) // For debugging, Remove when final build is done
		{
			taskDataList = new();
			SaveTaskListToPlayerPref(taskDataList);
			ClearPlayerPref_TaskList = false;
			Debug.Log($"Cleared PlayerPrefs TaskList @ Task System Game Object: {gameObject.name}");
		}

		// Load Scriptable Objects from Resources
		MasterTaskSOList = Resources.LoadAll<TaskSO>(TASK_SO_PATH).ToHashSet();

		// Load the task list data from PlayerPrefs
		taskDataList = LoadTasksFromPlayerPrefs();


	}

	private void Start()
	{
		// Invoke the event to refresh all the listeners
		OnTaskListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			TaskList = taskDataList
		});
	}

	/// <summary>
	/// Get the task ScriptableObject from the task data.
	/// </summary>
	/// <param name="taskData"></param>
	/// <param name="taskSO"></param>
	/// <returns></returns>
	public bool GetTaskSO(TaskData taskData, out TaskSO taskSO)
	{
		// If task data is not in the master list, return false
		if (!MasterTaskSOList.Any(taskSO => taskSO.ID == taskData.TaskID))
		{
			taskSO = null;
			return false;
		}

		// Get the TaskSO from the master list
		taskSO = MasterTaskSOList.First(taskSO => taskSO.ID == taskData.TaskID);
		return true;
	}



	public void AddTask(TaskSO task)
	{
		// If task is not in master list or task is already in the list, do not add the task to the list
		if (!MasterTaskSOList.Contains(task) || taskDataList.Any(taskData => taskData.TaskID == task.ID)) return;

		TaskData newTaskData = new TaskData(task);

		int firstIndex = 0;
		taskDataList.Insert(firstIndex, newTaskData); // Put it at the start of the list

		SaveTaskListToPlayerPref(taskDataList);

		OnTaskListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			TaskList = taskDataList
		});

	}

	/// <summary>
	/// Save the task list to PlayerPrefs.
	/// </summary>
	/// <param name="_taskList"></param>
	private void SaveTaskListToPlayerPref(List<TaskData> _taskList)
	{
		// Serialize the taskList to JSON
		string json = JsonUtility.ToJson(new TaskListWrapper { Tasks = _taskList });
		PlayerPrefs.SetString(TASK_LIST_KEY, json);
		PlayerPrefs.Save(); // Ensure PlayerPrefs is saved
	}

	/// <summary>
	/// Load the task list from PlayerPrefs.
	/// </summary>
	/// <returns></returns>
	public List<TaskData> LoadTasksFromPlayerPrefs()
	{
		List<TaskData> _taskList = new List<TaskData>();
		if (PlayerPrefs.HasKey(TASK_LIST_KEY)) // If the key exists in PlayerPrefs
		{
			string json = PlayerPrefs.GetString(TASK_LIST_KEY);
			TaskListWrapper wrapper = JsonUtility.FromJson<TaskListWrapper>(json);
			_taskList = wrapper.Tasks;
		}
		else // If the key does not exist in PlayerPrefs, Save an empty one to PlayerPrefs
			SaveTaskListToPlayerPref(_taskList);

		OnTaskListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			TaskList = _taskList
		});

		return _taskList;
	}

	private void OnDestroy()
	{
		SaveTaskListToPlayerPref(taskDataList);
	}


	public void FinishTask(TaskSO task)
	{
		// If task is not in master list or task is NOT already in the list, do not finish the task
		if (!MasterTaskSOList.Contains(task) || !taskDataList.Any(taskData => Equals(taskData.TaskID, task.ID))) return;

		TaskData newTaskData = new TaskData(task);

		// Check if the task is in the list and if it is done
		if (newTaskData.IsDone) return;

		// Mark as done
		newTaskData.IsDone = true;

		// Remove
		taskDataList.RemoveAll(taskData => taskData.TaskID == task.ID);

		// Remove the required item of the task in the inventory
		InventorySystem.Instance.RemoveItem(task.requiredItem);


		// Insert at the end
		int lastIndex = taskDataList.Count;
		taskDataList.Insert(lastIndex, newTaskData); // Put it at the end of the list

		SaveTaskListToPlayerPref(taskDataList);
		LoadTasksFromPlayerPrefs();

		OnTaskListChanged?.Invoke(this, new OnItemListChangedEventArgs
		{
			TaskList = taskDataList
		});
	}


	/// <summary>
	/// Wrapper class to serialize the task list to JSON.
	/// </summary>
	[Serializable]
	private class TaskListWrapper
	{
		public List<TaskData> Tasks;
	}

}
