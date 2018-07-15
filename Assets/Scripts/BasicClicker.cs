using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicClicker : MonoBehaviour {
	public float BrainPower = 0f;
	public TextMesh PowerLabel;
	public TextMesh TimeLabel;

	void OnMouseDown () {
		if (Input.GetMouseButtonDown(0)) {
			BrainPower += 1f;
		}
	}

	void Update() {
		PowerLabel.text = BrainPower.ToString("N0");
		TimeLabel.text = string.Format("{0}:{1}", Mathf.FloorToInt(Time.timeSinceLevelLoad / 60), 
			Mathf.RoundToInt(Time.timeSinceLevelLoad % 60).ToString("00"));
	}
}
