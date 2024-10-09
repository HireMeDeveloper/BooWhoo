using TMPro;
using UnityEngine;


public class TooltipUI : MonoBehaviour
{
	public static TooltipUI Instance { get; private set; }
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private RectTransform backgroundRectTransform;
	[SerializeField] private RectTransform nameTextRectTransform;
	[SerializeField] private Transform content;


	private void Awake()
	{
		Instance = this;
		HideTooltip();

	}
	public void ShowTooltip(string name)
	{
		// Set Content
		nameText.text = name;

		// Set size of content
		float textPaddingSize = 4f;
		Vector2 backgroundSize = new Vector2(nameText.preferredWidth + textPaddingSize * 4, nameText.preferredHeight + textPaddingSize);
		backgroundRectTransform.sizeDelta = backgroundSize;
		nameTextRectTransform.sizeDelta = backgroundSize;

		// Enable content
		content.gameObject.SetActive(true);


	}


	private void Update()
	{
		if (!content.gameObject.activeSelf) return;

		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			transform.parent.GetComponent<RectTransform>(),
			Input.mousePosition,
		 	null,
		 	 out Vector2 localPoint);

		localPoint.y -= 50f; // Y offset
		transform.localPosition = localPoint;
	}

	public void HideTooltip()
	{
		content.gameObject.SetActive(false);
	}

	public void SetTooltipInfo(string name)
	{

		if (name == "")
		{
			HideTooltip();
			return;
		}
		ShowTooltip(name);
	}

}
