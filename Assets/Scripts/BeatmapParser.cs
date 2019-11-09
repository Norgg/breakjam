using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class BeatmapParser : MonoBehaviour {

    public TextAsset file;
    Beatmap map;

    double timer = 0;
    double timeBetweenBeats;
    int beat = 0;

    float noteOffsetDistance = 10;
    public Note notePrefab;


    // Start is called before the first frame update
    void Start() {
        string input = file.text;
        input = input.Replace("\t","    ");

        var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();

        map = deserializer.Deserialize<Beatmap>(input);

        timeBetweenBeats = 1.0 / (map.bpm / 60.0);

        AudioSource player = GetComponent<AudioSource>();
        player.PlayDelayed(map.intro);
    }

    // Update is called once per frame
    void Update() {
        if (timer >= timeBetweenBeats) {
            timer -= timeBetweenBeats;
            SpawnBeat();
            beat++;
        }

        if (beat >= map.beatCodes.Count) { this.enabled=false; }
        timer += Time.deltaTime;
    }


    void SpawnBeat() {
        string code = map.beatCodes[beat].code;
        if (code == "0") { return; }

        Note newNote = GameObject.Instantiate(notePrefab);

        if (code[0] == 'u') {
            newNote.dir = NoteSpawner.Direction.Up;
        }
        if (code[0] == 'd') {
            newNote.dir = NoteSpawner.Direction.Down;
        }

        if (code[0] == 'l') {
            newNote.dir = NoteSpawner.Direction.Left;
        }

        if (code[0] == 'r') {
            newNote.dir = NoteSpawner.Direction.Right;
        }

        var offset = new Vector3();
        switch (newNote.dir) {
            case NoteSpawner.Direction.Up:
                offset = Vector3.up * noteOffsetDistance;
                break;
            case NoteSpawner.Direction.Down:
                offset = Vector3.down * noteOffsetDistance;
                break;
            case NoteSpawner.Direction.Left:
                offset = Vector3.left * noteOffsetDistance;
                break;
            case NoteSpawner.Direction.Right:
                offset = Vector3.right * noteOffsetDistance;
                break;
        }

        newNote.transform.position = transform.position + offset;

        newNote.num = (int)char.GetNumericValue(code[1]);
    }
}
