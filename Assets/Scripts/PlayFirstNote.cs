using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFirstNote : MonoBehaviour
{

    BeatmapParser player;

    private void Awake() {
        player = GameObject.FindObjectOfType<BeatmapParser>();
    }

    void OnTriggerEnter(Collider col) {
        var note = col.transform.GetComponent<Note>();
        if (note) {
            player.Play();
            Destroy(this);
        }
    }
}
