using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackF1 : MonoBehaviour
{
    public GameObject F1Key;
    GameFeedbacker feedbacker;

    // Start is called before the first frame update
    void Start()
    {
        feedbacker = new GameFeedbacker("Fret1", F1Key);
    }

    // Update is called once per frame
    void Update()
    {
        feedbacker.Update();
    }
}
