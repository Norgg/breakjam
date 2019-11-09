using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {
    TextMeshPro text;
    public TextMeshPro percentageText;
    int hits = 0;
    int misses = 0;
    int score;
    int mult;

    void Start() {
        text = GetComponent<TextMeshPro>();
    }

    public void Hit() {
        hits++;
        score += 5 * mult;
        mult++;
        UpdateText();
    }

    public void Miss() {
        misses++;
        Mathf.Max(mult /= 2, 1);
        UpdateText();
    }

    void UpdateText() {
        var percent = 100.0f * (float) hits / (float) (hits + misses);
        percentageText.SetText(Mathf.FloorToInt(percent) + "%");

        text.SetText("" + score);
    }
}
