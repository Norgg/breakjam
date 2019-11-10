using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackF3 : MonoBehaviour
{
    public GameObject F3Key;
    GameFeedbacker feedbacker;

    // Start is called before the first frame update
    void Start()
    {
        feedbacker = new GameFeedbacker("Fret3", F3Key);
    }

    // Update is called once per frame
    void Update()
    {
        feedbacker.Update();
    }
}
