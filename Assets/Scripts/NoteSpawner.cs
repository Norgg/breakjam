using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour {
    public GameObject NotePrefab;

    enum Direction {
        Up,
        Down,
        Left,
        Right
    }

    float noteOffsetDistance = 10;

    float timeUntilNextBeat = 1;
    float nextBeatTimer = 0;

    void Update() {
        if (nextBeatTimer > 0) {
            nextBeatTimer -= Time.deltaTime;
        } else {
            var dir = (Direction)Random.Range(0, System.Enum.GetValues(typeof(Direction)).Length);
            var offset = new Vector3();
        
            switch(dir) {
                case Direction.Up:
                    offset = Vector3.up * noteOffsetDistance;
                    break;
                case Direction.Down:
                    offset = Vector3.down * noteOffsetDistance;
                    break;
                case Direction.Left:
                    offset = Vector3.left * noteOffsetDistance;
                    break;
                case Direction.Right:
                    offset = Vector3.right * noteOffsetDistance;
                    break;
            }
            var note = Instantiate(NotePrefab, transform.position + offset, Quaternion.identity);
            note.GetComponent<Note>().num = Random.Range(1, 4);
            nextBeatTimer = timeUntilNextBeat;
        }
    }
}
