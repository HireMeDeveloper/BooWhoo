using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

	// XXX: Unsure if this is necessary or should put it in an Actual Item?
	public void OnPointerExit(PointerEventData eventData)
	{
		if (TooltipUI.Instance == null) return;
		TooltipUI.Instance.SetTooltipInfo("");
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (InventorySystem.Instance == null || InventorySystem.Instance.SelectedItem == null || TooltipUI.Instance == null) return;
		ItemSO item = InventorySystem.Instance.SelectedItem;


		string message = $"{item.Name}\n{item.Description}";
		TooltipUI.Instance.SetTooltipInfo(message);
	}
}
