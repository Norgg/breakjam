using System;
using UnityEngine;

public class GameFeedbacker
{
    private readonly string key;
    private readonly GameObject gameObject;

    public GameFeedbacker(string key, GameObject gameObject)
    {
        this.key = key;
        this.gameObject = gameObject;
    }

    public void Update()
    {
        if (Input.GetButton(key))
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
