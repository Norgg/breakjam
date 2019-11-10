using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackF4 : MonoBehaviour
{
    public GameObject F4Key;
    GameFeedbacker feedbacker;

    // Start is called before the first frame update
    void Start()
    {
        feedbacker = new GameFeedbacker("Fret4", F4Key);
    }

    // Update is called once per frame
    void Update()
    {
        feedbacker.Update();
    }
}
