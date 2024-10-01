using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
	[SerializeField] private ItemSO itemSO;
	public Button button;
	private void Awake()
	{
		button.onClick.AddListener(() =>
		{
			Debug.Log(itemSO.ID);
		});
	}
}
