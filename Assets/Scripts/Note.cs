using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note : MonoBehaviour {
    NoteSpawner spawner;
    TextMeshPro text;
    float speed = 0.03f;
    public int num;

    void Start() {
        spawner = GameObject.FindObjectOfType<NoteSpawner>();
        text = GetComponentInChildren<TextMeshPro>();
    }

    void Update() {
        var directionToSpawner =  spawner.transform.position - transform.position;
        directionToSpawner.Normalize();
        directionToSpawner *= speed;
        text.SetText("" + num);

        transform.position += directionToSpawner;

        if (Vector3.Distance(transform.position, spawner.transform.position) < 0.1f) {
            Destroy(gameObject);
        }
    }
}
