using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class BeatmapParser : MonoBehaviour {

    public TextAsset file;
    Beatmap map;

    float timeBetweenBeats;

    public Note notePrefab;

    void Awake() {

        GameObject selected = GameObject.Find("TrackHolder");
        if (selected != null) {
            ChosenTrack track = selected.GetComponent<ChosenTrack>();
            if (track != null) {
                file = track.chosenTrack;
            }
            Destroy(selected);
        }



        string input = file.text;
        input = input.Replace("\t", "    ");

        var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();

        map = deserializer.Deserialize<Beatmap>(input);

        timeBetweenBeats = 1.0f / (map.bpm / 60.0f);


        AudioClip song = null;

        string path = "Tracks/Musics";


        var musicFiles = Resources.LoadAll<AudioClip>(path);


        foreach (var s in musicFiles) {
            if (s.name == map.song) {
                song = s;
                break;
            }
        }

        if (song == null) {
            Debug.LogError("Could not find song " + map.song);
        }

        AudioSource player = GetComponent<AudioSource>();
        player.clip = song;

        Note lastNote = null;
        for (var i = 0; i < map.beatCodes.Count; i++) {
            var beat = map.beatCodes[i];
            var thisNote = SpawnBeat(beat, i, timeBetweenBeats, map.intro);
            if (thisNote != null) {
                if (lastNote != null) {
                    lastNote.last = false;
                }
                lastNote = thisNote;
            }
        }

        // InvokeRepeating("SpawnBeat",0f,(float)timeBetweenBeats);
    }

    Note SpawnBeat(Beat beat, int beatNum, float timeBetweenBeats, float introTime) {
        string code = beat.code;
        if (code == "0") { return null; }

        Note newNote = GameObject.Instantiate(notePrefab);
        newNote.num = (int)char.GetNumericValue(code[1]);
        newNote.speed = map.speed;
        newNote.beatNum = beatNum;
        newNote.last = true;

        var offset = Vector3.zero;
        offset += Vector3.right * (-10f + 4f * newNote.num);
        var vertOffset = Vector3.up * (timeBetweenBeats * beatNum * newNote.speed + 3.0f * newNote.speed);
        newNote.transform.position = transform.position + offset + vertOffset;
        return newNote;
    }


    public void Play() {
        AudioSource player = GetComponent<AudioSource>();
        player.PlayDelayed(map.intro);
    }
}
