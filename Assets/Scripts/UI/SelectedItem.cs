using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItem : MonoBehaviour
{
	[SerializeField] private Image sprite;
	private ItemSO itemSO;

	private void Start()
	{
		InventorySystem.Instance.OnChangeSelectedItem += Inventory_OnChangeSelectedItem;
	}

	private void Inventory_OnChangeSelectedItem(object sender, InventorySystem.OnChangeSelectedItemEventArgs e)
	{
		itemSO = e.Item;
		sprite.sprite = itemSO.SpriteIcon;
	}
}
