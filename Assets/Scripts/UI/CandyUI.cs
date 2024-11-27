using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CandyUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI candyAmountText;

	private void Start()
	{
		candyAmountText.text = "0";
		CandyInventory.OnCandyAmountChanged += CandyInventory_OnCandyAmountChanged;
	}
	private void Disable()
	{
		CandyInventory.OnCandyAmountChanged -= CandyInventory_OnCandyAmountChanged;
	}

	private void CandyInventory_OnCandyAmountChanged(object sender, CandyInventory.OnCandyAmountChangedEventArgs e)
	{
		candyAmountText.text = e.CandyAmount.ToString();
	}
}
