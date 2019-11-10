using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackF2 : MonoBehaviour
{
    public GameObject F2Key;
    GameFeedbacker feedbacker;

    // Start is called before the first frame update
    void Start()
    {
        feedbacker = new GameFeedbacker("Fret2", F2Key);
    }

    // Update is called once per frame
    void Update()
    {
        feedbacker.Update();
    }
}
