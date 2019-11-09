using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {
    public TextMeshPro text;
    int hits = 0;
    int misses = 0;

    void Start() {
        text = GetComponent<TextMeshPro>();
    }

    public void Hit() {
        hits++;
        UpdateText();
    }

    public void Miss() {
        misses++;
        UpdateText();
    }

    void UpdateText() {
        float percent = 100.0f * (float) hits / (float) (hits + misses);
        text.SetText("" + Mathf.FloorToInt(percent));
    }
}
