using System.Collections;
using UnityEngine;

public class BrainPart : MonoBehaviour
{
    public int Index;
    public Sequencer Sequencer;

    void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Sequencer.BrainPartClicked(Index);
        }
    }

    void Update() {

    }

    public void Flash() {
        StartCoroutine(FadeTo(0f, 1f));
    }

    IEnumerator FadeTo(float aValue, float aTime)
     {
         float alpha = 1f;//transform.GetComponent<SpriteRenderer>().material.color.a;
         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
             transform.GetComponent<SpriteRenderer>().color = newColor;
             yield return null;
         }
     }
}