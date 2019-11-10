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
    Renderer render;

    public bool broken = false;
    public bool last = true;

    Score score;

    void Start() {
        spawner = GameObject.FindObjectOfType<BeatmapParser>();
        text = GetComponentInChildren<TextMeshPro>();
        detection = GameObject.Find("DetectionZone");
        core = GameObject.Find("Core");
        animator = GetComponentInChildren<Animator>();
        score = FindObjectOfType<Score>();
        animator.transform.RotateAround(animator.transform.position, Vector3.forward, Random.Range(0, 360));
        render = GetComponentInChildren<Renderer>();
        render.enabled = false;
        animator.enabled = false;
        text.enabled = false;
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
        if (last) {
            score.TrackFinished();
        }
    }

    public void Hit(bool good) {
        // A successful note hit
        if (broken) return;
        broken = true;
        text.enabled = false;
        animator.speed = 2.2f;
        animator.Play("implode");
        GameObject.Destroy(gameObject, 0.3f);
        score.Hit(good);
        if (last) {
            score.TrackFinished();
        }
    }

    void FixedUpdate() {
        if (broken) return;

        if (transform.position.y < 10 & !broken) {
            render.enabled = true;
            animator.enabled = true;
            text.enabled = true;
        }

        text.SetText("" + num);

        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
