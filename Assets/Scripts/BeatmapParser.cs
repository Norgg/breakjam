using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class BeatmapParser : MonoBehaviour {

    public TextAsset file;
    Beatmap map;

    // Start is called before the first frame update
    void Start() {
        string input = file.text;

        var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();

        map = deserializer.Deserialize<Beatmap>(input);

        Debug.Log(map.bpm);
        Debug.Log(map.beatCodes);

        

    }

    // Update is called once per frame
    void Update() {

    }
}
