using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Item Scriptable Object (ItemSO) is a class that stores a developer defined item.
/// </summary>
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
	[ReadOnly] public string ID;

	[ReadOnly] public string ItemName;
	public Sprite SpriteIcon;

	public string Description;


	public bool IsValid { get { return string.IsNullOrEmpty(ID); } }


#if UNITY_EDITOR
	private void OnValidate()
	{
		// Generate name to be same as the asset file name
		string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
		ItemName = Path.GetFileNameWithoutExtension(assetPath);

		if (string.IsNullOrEmpty(ID)) // If the ID is empty, generate a new one
		{
			ID = GUID.Generate().ToString();
			UnityEditor.EditorUtility.SetDirty(this);
		}
	}
#endif

}


