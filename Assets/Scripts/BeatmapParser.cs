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
    public Note note;
    


    // Start is called before the first frame update
    void Start() {
        string input = file.text;
        input = input.Replace("\t","    ");

        var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();

        map = deserializer.Deserialize<Beatmap>(input);

        timeBetweenBeats = 1.0 / (map.bpm / 60.0);


      

        AudioClip song = null;

        SongNames dict = GetComponent<SongNames>();

        for(int i =0;i<dict.songNames.Length;i++) {
            string s = dict.songNames[i];
            if (s == map.song) {
                song = dict.songFiles[i];
            }
        }

        if (song == null) {
            Debug.LogError("Could nto find song "+map.song);
        }

        AudioSource player = GetComponent<AudioSource>();
        player.clip = song;

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

        Note newNote = GameObject.Instantiate(note);
        newNote.speed = map.speed;

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


        note.transform.position = transform.position + offset;

        newNote.num = (int)char.GetNumericValue(code[1]);

    }
}
