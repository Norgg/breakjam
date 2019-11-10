using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TrackList : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Button buttonTemplate;

    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        int y = 0;
        int height = 44;
        var path = "Resources/Tracks/Beatmaps";
        var textFiles = Resources.LoadAll(path, typeof(TextAsset));
        var trackNames = new List<string>();

        foreach (var textFile in textFiles)
        {
            trackNames.Add(textFile.name);
        }

        foreach (var title in trackNames)
        {
            var newButton = Instantiate(buttonTemplate);
            var textMesh = newButton.GetComponentInChildren<TextMeshProUGUI>();

            textMesh.text = title;

            newButton.transform.parent = scrollRect.transform;
            newButton.transform.localPosition = new Vector3(x, y, 0);

            y += height;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
