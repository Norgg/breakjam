﻿using System.Collections;
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

        SongNames dict = GetComponent<SongNames>();

        for (int i = 0; i < dict.songNames.Length; i++) {
            string s = dict.songNames[i];
            if (s == map.song) {
                song = dict.songFiles[i];
            }
        }

        if (song == null) {
            Debug.LogError("Could not find song " + map.song);
        }

        AudioSource player = GetComponent<AudioSource>();
        player.clip = song;

        for (var i = 0; i < map.beatCodes.Count; i++) {
            var beat = map.beatCodes[i];
            SpawnBeat(beat, i, timeBetweenBeats, map.intro);
        }

        // InvokeRepeating("SpawnBeat",0f,(float)timeBetweenBeats);
    }

    void SpawnBeat(Beat beat, int beatNum, float timeBetweenBeats, float introTime) {
        string code = beat.code;
        if (code == "0") { return; }

        Note newNote = GameObject.Instantiate(notePrefab);
        newNote.num = (int)char.GetNumericValue(code[1]);
        newNote.speed = map.speed;
        newNote.beatNum = beatNum;

        // if (code[0] == 'u') {
        //     newNote.dir = NoteSpawner.Direction.Up;
        // }
        // if (code[0] == 'd') {
        //     newNote.dir = NoteSpawner.Direction.Down;
        // }

        // if (code[0] == 'l') {
        //     newNote.dir = NoteSpawner.Direction.Left;
        // }

        // if (code[0] == 'r') {
        //     newNote.dir = NoteSpawner.Direction.Right;
        // }

        var offset = Vector3.zero;
        offset += Vector3.right * (-10f + 4f * newNote.num);
        var vertOffset = Vector3.up * (timeBetweenBeats * beatNum * newNote.speed + 3.0f * newNote.speed);
        newNote.transform.position = transform.position + offset + vertOffset;
    }


    public void Play() {
        AudioSource player = GetComponent<AudioSource>();
        player.PlayDelayed(map.intro);
    }
}
