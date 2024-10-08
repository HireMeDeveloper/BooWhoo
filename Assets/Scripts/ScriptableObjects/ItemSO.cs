using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{

	public Guid ID { get; private set; }
	[ReadOnly] public string StringID;

	[ReadOnly] public string Name; // Rename to ItemName? to follow TaskSO's Name format
	public Sprite SpriteIcon;

	public string Description;


	public bool IsValid { get { return ID != Guid.Empty; } }
	public ItemSO()
	{
		if (ID == Guid.Empty) // If the ID is empty, generate a new one
			ID = Guid.NewGuid();

	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		// Generate name to be same as the asset file name
		string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
		Name = Path.GetFileNameWithoutExtension(assetPath);

		StringID = ID.ToString(); // Refresh the string ID
	}
#endif


}
