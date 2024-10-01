using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{

	public Guid ID { get; private set; }

#if UNITY_EDITOR
	[ReadOnly] public string StringID;
#endif

	[ReadOnly] public string Name;
	public Sprite SpriteIcon;

	[Tooltip("Multiple descriptions can be added, each will be displayed on a new line.")]
	public string[] Description;

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
