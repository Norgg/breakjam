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
    }

    public void Hit(bool good) {
        // A successful note hit
        if (broken) return;
        broken = true;
        score.Hit(good);
        GameObject.Destroy(gameObject);
    }

    void FixedUpdate() {
        if (transform.position.y < 10) {
            render.enabled = true;
            animator.enabled = true;
            text.enabled = true;
        }

        /*var directionToSpawner =  spawner.transform.position - transform.position;
        directionToSpawner.Normalize();
        directionToSpawner *= speed;*/
        text.SetText("" + num);

        if (!broken) {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        //transform.position += directionToSpawner;
    }
}
