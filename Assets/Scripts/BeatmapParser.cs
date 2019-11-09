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

    public Note note;

    // Start is called before the first frame update
    void Start() {
        string input = file.text;

        var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();

        map = deserializer.Deserialize<Beatmap>(input);

        timeBetweenBeats = 1.0 / (map.bpm / 60.0);




    }

    // Update is called once per frame
    void Update() {
        if (timer >= timeBetweenBeats) {
            timer -= timeBetweenBeats;
            SpawnBeat();
            beat++;

        }
        timer += Time.deltaTime;
    }


    void SpawnBeat() {
        Note newNote = GameObject.Instantiate(note);
        string code = map.beatCodes[beat].code;
        if (code[0] == 'u') {
            newNote.dir =NoteSpawner.Direction.Up;
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

        newNote.num = (int)char.GetNumericValue(code[1]);

    }
}
