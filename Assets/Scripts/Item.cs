using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
	public ItemSO ItemData { get; private set; }
	[SerializeField] private TextMeshProUGUI itemDescription;


	private void Awake()
	{
	}
	private void Start()
	{
		SetItemData(ItemData);
	}

	/// <summary>
	/// Set item data to the UI
	/// </summary>
	/// <param name="item"></param>
	public void SetItemData(ItemSO item)
	{
		ItemData = item;
		if (item != null)
		{
			itemDescription.text = ItemData.Description;
		}
		else
		{
			itemDescription.text = "";
		}
	}

}
