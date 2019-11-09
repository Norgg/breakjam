using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {
    List<Note> notesThisFrame;
    int numFrets = 4;

    void Start() {
        notesThisFrame = new List<Note>();
    }

    void Update() {
        if (notesThisFrame.Count > 0) {
            // Only check the most recent note for each fret
            for (var fret = 1; fret <= numFrets; fret++) {
                var fretNotes = notesThisFrame.FindAll(note => note.num == fret);
                fretNotes.Sort((a, b) => a.beatNum - b.beatNum);
                if (fretNotes.Count > 0) {
                    var soonestNote = fretNotes[0];
                    checkNote(soonestNote);
                }
            }
            notesThisFrame.Clear();
        }
    }

    void checkNote(Note note) {
        var dirButton = note.dir.ToString();
        var badFrets = false;
        for (var i = 1; i <= numFrets; i++) {
            if (i != note.num && Input.GetButton("Fret" + i)) {
                badFrets = true;
                break;
            }
        }

        if (!badFrets && Input.GetButton("Fret" + note.num) && Input.GetButtonDown(dirButton)) {
            note.Hit();
        }
    }

    void OnTriggerEnter(Collider col) {
        var note = col.transform.GetComponent<Note>();
        if (note && !note.broken) {
            notesThisFrame.Add(note);
        }
    }
    
    void OnTriggerStay(Collider col) {
       var note = col.transform.GetComponent<Note>();
        if (note && !note.broken) {
            notesThisFrame.Add(note);
        }
    }
}
