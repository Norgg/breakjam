using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class TrackList : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Button buttonTemplate;
    public GameObject chosenTrack;

    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        int y = 0;
        int height = 64;
        int width = 1000;
        var path = "Tracks/Beatmaps";
        var textFiles = Resources.LoadAll<TextAsset>(path);

        foreach (var textFile in textFiles)
        {
            var newButton = Instantiate<Button>(buttonTemplate);
            var textMesh = newButton.GetComponentInChildren<TextMeshProUGUI>();

            textMesh.text = textFile.name;

            newButton.transform.parent = scrollRect.transform;
            //newButton
            //    .GetComponent<RectTransform>()
            //    .SetPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);
            newButton.transform.localPosition = new Vector3(x, y, 0);
            newButton.onClick.AddListener(() => OnTrackSelected(textFile));

            y += height + 10;
        }
    }

    private void OnTrackSelected(TextAsset textFile)
    {
        var obj = new GameObject();
        var go = GameObject.Instantiate(obj);
        go.name = "TrackHolder";
        go.AddComponent<ChosenTrack>();
        go.GetComponent<ChosenTrack>().chosenTrack = textFile;
        Object.DontDestroyOnLoad(go);
        SceneManager.LoadScene("level");

    }
}
