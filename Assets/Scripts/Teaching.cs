using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teaching : MonoBehaviour {
	public Scorer Scorer;
	public Button Button;
	public Text CostLabel;
	public Text QuantityLabel;
	public int Cost;
	public float PowerGainPerUnit;

	public int Quantity = 0;
	
	// Update is called once per frame
	void Update () {
		if (Scorer.CurrentScore < Cost) {
			Button.interactable = false;
		} else {
			Button.interactable = true;
		}

		CostLabel.text = Cost.ToString("N0");
		QuantityLabel.text = Quantity.ToString("N0");
	}

	public void DoClick() {
		Scorer.AddScore(-Cost);
		Quantity += 1;
		Cost += Cost * 20 / 100;
	}

	public float GetGain() {
		return PowerGainPerUnit * Quantity; 
	}
}
