using System;
using System.IO;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Task Scriptable Object (TaskSO) is a class that stores a developer defined task.
/// </summary>
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Task")]
public class TaskSO : ScriptableObject
{
	[ReadOnly] public string ID;

	[ReadOnly] public string TaskName;

	public string SingleLineDescription;
	public string Description;

	[Tooltip("Required item to complete tshe task")]
	public ItemSO requiredItem;

#if UNITY_EDITOR
	private void OnValidate()
	{
		// Generate name to be same as the asset file name
		string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
		TaskName = Path.GetFileNameWithoutExtension(assetPath);

		if (string.IsNullOrEmpty(ID)) // If the ID is empty, generate a new one
		{
			ID = GUID.Generate().ToString();
			UnityEditor.EditorUtility.SetDirty(this);
		}
	}
#endif
}

/// <summary>
/// TaskData is a serializable class that stores the runtime data of a task.
/// </summary>
[System.Serializable]
public class TaskData
{
	public string TaskID; // Use the unique ID from TaskSO
	public bool IsDone; // Runtime data (whether the task is done)

	public TaskData(TaskSO task)
	{
		TaskID = task.ID; // Store the task's unique identifier
		IsDone = false; // Default is false, can be updated
	}
}
