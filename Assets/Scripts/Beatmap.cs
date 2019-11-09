using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beatmap 
{
    
    public int bpm { get; set; }

    public float intro { get; set; }

    public string song { get; set; }

    public float speed { get; set; }

    [YamlDotNet.Serialization.YamlMember(Alias = "beat_codes", ApplyNamingConventions = false)]
    public List<Beat> beatCodes { get; set; }    


}

public class Beat {
   
    public string code { get; set; }

}

