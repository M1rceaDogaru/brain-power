using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    public List<Teaching> Teachings;
    public Teaching PowerGainTeaching;
    public TextMesh ScoreGainTextPrefab;
    public float ScoreGainMoveSpeed = 20f;
    public float CurrentScore = 0f;
    public Status Status;

    private float lastTeachingGain = 0f;

    void Update() {
        var gain = 0f;
        foreach(var teaching in Teachings) {
            gain += teaching.GetGain();
        }

        lastTeachingGain -= Time.deltaTime;
        if (lastTeachingGain <= 0) {
            lastTeachingGain = 1f;
            CurrentScore += gain;
        }

        Status.SetGain(gain);
        Status.SetPower(CurrentScore);
    }

    public void AddScoreWithAnimation() {
        AddScore(PowerGainTeaching.GetGain());
    }

    public void RemoveScoreWithAnimation() {
        AddScore(-PowerGainTeaching.GetGain());
    }

    public void AddScore(float value) {
        CurrentScore += value;
        ShowScoreGain(value);
    }

    private void ShowScoreGain(float value) {
        var worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(
            Input.mousePosition.x,
            Input.mousePosition.y
        ));

        var scoreGainText = Instantiate(ScoreGainTextPrefab, 
            new Vector3(worldPosition.x, worldPosition.y, -3f),
            Quaternion.identity);
        
        if (value > 0) {
            scoreGainText.color = new Color(0, 255, 0);
        } else {
            scoreGainText.color = new Color(255, 0, 0);
        }
        
        scoreGainText.text = string.Format("{0}{1:N0}",
            value > 0 ? "+" : "-",
            Mathf.Abs(value));

        StartCoroutine(AnimateScoreGain(scoreGainText));
    }

    private IEnumerator AnimateScoreGain(TextMesh scoreGain) {
        float alpha = 1f;//transform.GetComponent<SpriteRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1f)
        {
            var newColor = new Color(scoreGain.color.r, scoreGain.color.g, scoreGain.color.b,
            Mathf.Lerp(alpha, 0f, t));
            scoreGain.color = newColor;

            scoreGain.transform.position = new Vector3(scoreGain.transform.position.x,
                scoreGain.transform.position.y + ScoreGainMoveSpeed * Time.deltaTime,
                scoreGain.transform.position.z);
            
            yield return null;
        }

        Destroy(scoreGain.gameObject);
    }
}