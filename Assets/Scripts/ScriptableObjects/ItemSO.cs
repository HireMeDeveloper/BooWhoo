using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{

	public Guid ID { get; private set; }

	[ReadOnly] public string StringID;

	public string Name;
	public Sprite SpriteIcon;
	public string Description;

	public ItemSO()
	{
		ID = Guid.NewGuid();
		StringID = ID.ToString();
		Debug.Log($"ItemSO Constructor: {ID}");
	}


}
