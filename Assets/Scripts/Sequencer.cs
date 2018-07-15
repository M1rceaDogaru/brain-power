using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour {
    public Scorer Scorer;
    public Status Status;
    public List<BrainPart> BrainParts;
    public int SequenceLength = 2;
    public float SequenceDuration = 10f;
    public List<int> SequenceLengthChangesPerPower;

    public List<int> CurrentSequence;

    private float _currentSequenceTime = 0f;
    private int _currentSequenceIndex;
    
    void Start() {
        CurrentSequence = new List<int>();

        for(var i = 0; i < BrainParts.Count; i++) {
            BrainParts[i].Index = i;
        }
    }

    private void CheckSequenceLength() {
        var steps = 2;
        for (int i = 0; i < SequenceLengthChangesPerPower.Count; i++) {
            if (Scorer.CurrentScore <= SequenceLengthChangesPerPower[i]) {
                steps += i;
                break;
            }
        }

        if (SequenceLength < steps) {
            SequenceLength = steps;
        }
        Status.SetSequenceLength(SequenceLength);
    }
    
    void Update() {
        CheckSequenceLength();
        CheckSequence();
    }

    public void BrainPartClicked(int index) {
        //TODO: maybe add a denied sound when brain part clicked while a sequence is playing
        if (_isFlashingSequence) return;

        var currentBrainPartIndex = CurrentSequence[_currentSequenceIndex];
        if (index == currentBrainPartIndex) {
            Scorer.AddScoreWithAnimation();
            BrainParts[currentBrainPartIndex].Flash();
            if (_currentSequenceIndex == CurrentSequence.Count - 1) {
                _currentSequenceIndex = 0;
            } else {
                _currentSequenceIndex++;
            }
        } else {
            Scorer.RemoveScoreWithAnimation();
        }
    }

    private void CheckSequence() {
        if (_isFlashingSequence) return;

        _currentSequenceTime -= Time.deltaTime;
        if (_currentSequenceTime <= 0f) {            
            GenerateSequence();
            FlashSequence();
            _currentSequenceTime = SequenceDuration;
            _currentSequenceIndex = 0;
        } else if (_currentSequenceTime < 3f) {
            Status.SetStatus(string.Format("SEQUENCE CHANGE IN {0}...", Mathf.CeilToInt(_currentSequenceTime)));
        }
    }

    private void GenerateSequence() {
        CurrentSequence.Clear();
        for (var i = 0; i < SequenceLength; i++) {
            var brainPartIndex = Random.Range(0, BrainParts.Count);
            CurrentSequence.Add(brainPartIndex);
        }
    }

    private void FlashSequence() {
        _lastFlashedIndex = 0;
        for(var count = 0; count < CurrentSequence.Count; count++) {
            Invoke("FlashBrainPart", count);
        }
        _isFlashingSequence = true;
        Status.SetStatus("PLAYING NEW SEQUENCE...");
        Invoke("FlashSequenceStopped", CurrentSequence.Count);
    }

    private bool _isFlashingSequence;    
    private void FlashSequenceStopped() {
        _isFlashingSequence = false;
        Status.SetStatus("REPLAY THE SEQUENCE!");
    }

    private int _lastFlashedIndex;
    private void FlashBrainPart() {
        if (_lastFlashedIndex >= CurrentSequence.Count) return;

        var brainPartIndex = CurrentSequence[_lastFlashedIndex];
        BrainParts[brainPartIndex].Flash();
        _lastFlashedIndex++;
    }

}