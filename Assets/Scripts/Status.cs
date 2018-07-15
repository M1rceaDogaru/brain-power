using UnityEngine;

public class Status : MonoBehaviour {
    public TextMesh StatusLabel;

    public TextMesh PowerLabel;

    public TextMesh GainLabel;

    public TextMesh SequenceLengthLabel;

    public TextMesh TimeLabel;

    void Update() {
        TimeLabel.text = string.Format("{0}:{1}", Mathf.FloorToInt(Time.timeSinceLevelLoad / 60), 
			Mathf.RoundToInt(Time.timeSinceLevelLoad % 60).ToString("00"));
    }

    public void SetStatus(string value) {
        StatusLabel.text = value;
    }

    public void SetPower(float power) {
        PowerLabel.text = power.ToString("N0");
    }

    public void SetGain(float gain) {
        GainLabel.text = string.Format("-PER SECOND: {0:N2}-", gain);
    }

    public void SetSequenceLength(int length) {
        SequenceLengthLabel.text = string.Format("SEQUENCE LENGTH: {0:N0}", length);
    }
}