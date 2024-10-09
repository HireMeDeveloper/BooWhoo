using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Task : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public TaskSO ItemData { get; private set; }

	[SerializeField] private TextMeshProUGUI itemDescription;


	private void Start()
	{
		SetItemData(ItemData);
	}

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

	public void OnPointerExit(PointerEventData eventData)
	{
		if (TooltipUI.Instance == null) return;
		TooltipUI.Instance.SetTooltipInfo("");
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (TooltipUI.Instance == null || ItemData == null) return;
		string message = $"{ItemData.TaskName}\n{ItemData.Description}";
		TooltipUI.Instance.SetTooltipInfo(message);
	}
}
