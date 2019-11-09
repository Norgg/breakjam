using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {

    float damageTime = 0.2f;
    float damageTimer = 0;
    Color startColor = new Color(1, 1, 1, 1);
    Color damageColor = new Color(1, 0, 0, 1);
    Material mat;

    void Start() {
        mat = GetComponent<Renderer>().material;
    }

    void Update() {
        if (damageTimer > 0) {
            damageTimer -= Time.deltaTime;
            mat.color = Color.Lerp(startColor, damageColor, damageTimer / damageTime);
        }
    }

    void Damage(Note note) {
        Destroy(note.gameObject);
        damageTimer = damageTime;
    }

    void OnTriggerEnter(Collider col) {
        var note = col.transform.GetComponent<Note>();
        if (note) {
            Damage(note);
        }
    }
    
    void OnTriggerStay(Collider col) {
       var note = col.transform.GetComponent<Note>();
        if (note) {
            Damage(note);
        }
    }
}
