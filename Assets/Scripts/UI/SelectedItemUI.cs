using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private Image sprite;

	private void Start()
	{
		InventorySystem.OnSelectedItemChanged += Inventory_OnSelectedItemChanged;
	}

	private void Disable()
	{
		InventorySystem.OnSelectedItemChanged -= Inventory_OnSelectedItemChanged;
	}

	/// <summary>
	/// Set the sprite icon of the selected item
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Inventory_OnSelectedItemChanged(object sender, InventorySystem.OnSelectedItemChangedEventArgs e)
	{
		if (e.Item == null)
		{
			sprite.sprite = null;
			return;
		}
		sprite.sprite = e.Item.SpriteIcon;
	}

	/// <summary>
	/// Clear the item data when the mouse exits the item
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerExit(PointerEventData eventData)
	{
		if (TooltipUI.Instance == null) return;
		TooltipUI.Instance.SetTooltipInfo("");
	}

	/// <summary>
	/// Display the item data when the mouse hovers over the item
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (InventorySystem.Instance == null || InventorySystem.Instance.SelectedItem == null || TooltipUI.Instance == null) return;
		ItemSO item = InventorySystem.Instance.SelectedItem;


		string message = $"{item.ItemName}\n{item.Description}";
		TooltipUI.Instance.SetTooltipInfo(message);
	}
}
