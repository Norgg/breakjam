using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {
    void Start() {
    }

    void checkNote(Note note) {
        Debug.Log("CHECKING " + note);
        var dirButton = note.dir.ToString();
        if (Input.GetButton("Fret" + note.num) && Input.GetButton(dirButton)) {
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
