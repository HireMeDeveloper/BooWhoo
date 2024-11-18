using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Display task information
/// </summary>
public class Task : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public TaskSO ItemData { get; private set; }

	[SerializeField] private TextMeshProUGUI itemDescription;


	private void Start()
	{
		SetItemData(ItemData);
	}

	/// <summary>
	/// Set item data to the UI
	/// </summary>
	/// <param name="item"></param>
	public void SetItemData(TaskSO item)
	{
		ItemData = item;
		if (item != null)
		{
			itemDescription.text = ItemData.SingleLineDescription;
		}
		else
		{
			itemDescription.text = "";

		}
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
		if (TooltipUI.Instance == null || ItemData == null) return;
		string message = $"{ItemData.TaskName}\n{ItemData.Description}";
		TooltipUI.Instance.SetTooltipInfo(message);
	}
}
