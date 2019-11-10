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
    public int beatNum;

    GameObject core;
    GameObject detection;

    Animator animator;

    public bool broken = false;

    Score score;

    void Start() {
        spawner = GameObject.FindObjectOfType<BeatmapParser>();
        text = GetComponentInChildren<TextMeshPro>();
        detection = GameObject.Find("DetectionZone");
        core = GameObject.Find("Core");
        animator = GetComponentInChildren<Animator>();
        score = FindObjectOfType<Score>();
        animator.transform.RotateAround(animator.transform.position, Vector3.forward, Random.Range(0, 360));
    }
    
    public void Break() {
        // Oh no
        if (broken) return;
        broken = true;
        text.enabled = false;
        animator.speed = 3.0f;
        animator.Play("destroy");
        GameObject.Destroy(gameObject, 0.3f);
        score.Miss();
    }

    public void Hit() {
        // A successful note hit
        if (broken) return;
        broken = true;
        score.Hit();
        GameObject.Destroy(gameObject);
    }

    void FixedUpdate() {
        var directionToSpawner =  spawner.transform.position - transform.position;
        directionToSpawner.Normalize();
        directionToSpawner *= speed;
        text.SetText("" + num);

        if (!broken) {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        //transform.position += directionToSpawner;
    }
}
