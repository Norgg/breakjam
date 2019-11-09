using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note : MonoBehaviour {
    BeatmapParser spawner;
    TextMeshPro text;
    public float speed = 0.03f;

    public NoteSpawner.Direction dir;
    public int num;

    GameObject core;
    GameObject detection;

    Animator animator;

    public bool broken = false;

    void Start() {
        spawner = GameObject.FindObjectOfType<BeatmapParser>();
        text = GetComponentInChildren<TextMeshPro>();
        detection = GameObject.Find("DetectionZone");
        core = GameObject.Find("Core");
        animator = GetComponentInChildren<Animator>();
    }
    
    public void Break() {
        animator.Play("destroy");
        GameObject.Destroy(gameObject, 0.2f);
        broken = true;
    }

    void Update() {
        var directionToSpawner =  spawner.transform.position - transform.position;
        directionToSpawner.Normalize();
        directionToSpawner *= speed;
        text.SetText("" + num);

        if (!broken) {
            transform.position += Vector3.down * speed;
        }
        //transform.position += directionToSpawner;
    }
}
