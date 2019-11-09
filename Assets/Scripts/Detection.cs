using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {
    void Start() {
    }

    void checkNote(Note note) {
        var dirButton = note.dir.ToString();
        var badFrets = false;
        for (var i = 1; i < 5; i++) {
            if (i != note.num && Input.GetButton("Fret" + i)) {
                badFrets = true;
                break;
            }
        }

        if (!badFrets && Input.GetButton("Fret" + note.num) && Input.GetButton(dirButton)) {
            Destroy(note.gameObject);
        }
    }

    void OnTriggerEnter(Collider col) {
        var note = col.transform.GetComponent<Note>();
        if (note) {
            checkNote(note);
        }
    }
    
    void OnTriggerStay(Collider col) {
       var note = col.transform.GetComponent<Note>();
        if (note) {
            checkNote(note);
        }
    }
}
