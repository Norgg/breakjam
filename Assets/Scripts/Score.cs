using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour {
    TextMeshPro text;
    public TextMeshPro percentageText;
    public TextMeshPro comboText;
    public TextMeshPro completionText;
    int hits = 0;
    int misses = 0;
    int score;
    int mult;

    BeatmapParser parser;

    void Start() {
        text = GetComponent<TextMeshPro>();
        parser = GameObject.FindObjectOfType<BeatmapParser>();
    }

    public void Hit(bool good) {
        hits++;
        var addedScore = 5 * mult;
        if (good) addedScore *= 2;
        score += addedScore;
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

        comboText.SetText(mult + "x");

        text.SetText("" + score);
    }

    public void TrackFinished() {
        var track = parser.file.name;
        var scoreText = track + " complete, score: " + score;

        var scoreKey = track + ".score";
        var highScore = PlayerPrefs.GetInt(scoreKey, 0);
        if (score > highScore) {
            scoreText += "\nNew high score! Last high score was: " + highScore;
            PlayerPrefs.SetInt(scoreKey, score);
        }
        completionText.SetText(scoreText);
        Debug.Log(scoreText);

        Invoke("Reset", 5);
    }

    void Reset() {
        SceneManager.LoadScene(0);
    }
}
