using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {
    List<Note> notesThisFrame;
    int numFrets = 4;
    NoteSpawner.Direction lastDirection;
    float lastDirectionTime;
    float lastDirectionForgiveness = 0.05f;

    void Start() {
        notesThisFrame = new List<Note>();
    }

    void Update() {
        if (Input.GetButtonDown("Up")) {
            lastDirection = NoteSpawner.Direction.Up;
            lastDirectionTime = Time.time;
        } else if (Input.GetButtonDown("Down")) {
            lastDirection = NoteSpawner.Direction.Down;
            lastDirectionTime = Time.time;             
        } else if (Input.GetButtonDown("Left")) {
            lastDirection = NoteSpawner.Direction.Left;
            lastDirectionTime = Time.time;
        } else if (Input.GetButtonDown("Right")) {
            lastDirection = NoteSpawner.Direction.Right;
            lastDirectionTime = Time.time;
        }

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
        var badFrets = 0;
        for (var i = 1; i <= numFrets; i++) {
            if (i != note.num && Input.GetButton("Fret" + i)) {
                badFrets += 1;
                break;
            }
        }

        var dirButton = note.dir.ToString();
        bool buttonPress = Input.GetButtonDown(dirButton) || (lastDirection == note.dir && Time.time - lastDirectionTime < lastDirectionForgiveness);

        if (badFrets <= 2 && Input.GetButton("Fret" + note.num) && buttonPress) {
            lastDirectionTime = 0;
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
