using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class BeatmapParser : MonoBehaviour {

    public TextAsset file;
    Beatmap map;
    
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
        InvokeRepeating("SpawnBeat",0f,(float)timeBetweenBeats);
    }

  


    void SpawnBeat() {
        if (beat >= map.beatCodes.Count) { return; }
        string code = map.beatCodes[beat].code;
        if (code == "0") { beat++; return; }
        
        Note newNote = GameObject.Instantiate(notePrefab);
        newNote.num = (int)char.GetNumericValue(code[1]);
        newNote.speed = map.speed;

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

        // Just make them all come from up for now
        newNote.dir = NoteSpawner.Direction.Up;

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

        offset += Vector3.right * (-10f + 4f * newNote.num);
        newNote.transform.position = transform.position + offset;
        beat++;
       
    }


    public void Play() {
        AudioSource player = GetComponent<AudioSource>();
        player.PlayDelayed(map.intro);
    }
}
