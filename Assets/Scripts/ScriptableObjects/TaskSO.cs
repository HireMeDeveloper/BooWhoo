using System;
using System.IO;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Task")]
public class TaskSO : ScriptableObject
{
	public Guid ID { get; private set; }
	[ReadOnly] public string StringID;

	[ReadOnly] public string TaskName;

	public string Description;

	[Tooltip("Required item to complete the task")]
	public ItemSO requiredItem;

	public bool IsDone;

	public TaskSO()
	{
		if (ID == Guid.Empty) // If the ID is empty, generate a new one
			ID = Guid.NewGuid();
	}

	public void OnDisable()
	{
		IsDone = false;
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		// Generate name to be same as the asset file name
		string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
		TaskName = Path.GetFileNameWithoutExtension(assetPath);

		StringID = ID.ToString(); // Refresh the string ID
	}
#endif
}