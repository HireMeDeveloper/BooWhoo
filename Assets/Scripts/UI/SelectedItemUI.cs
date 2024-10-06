using UnityEngine;
using UnityEngine.UI;

public class SelectedItemUI : MonoBehaviour
{
	[SerializeField] private Image sprite;




	private void Start()
	{
		InventorySystem.Instance.OnSelectedItemChanged += Inventory_OnSelectedItemChanged;
	}

	private void Inventory_OnSelectedItemChanged(object sender, InventorySystem.OnSelectedItemChangedEventArgs e)
	{
		if (e.Item == null)
		{
			sprite.sprite = null;
			return;
		}
		sprite.sprite = e.Item.SpriteIcon;
	}
}
