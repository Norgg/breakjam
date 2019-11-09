using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionKeyPressListener : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)
            || Input.GetKeyDown(KeyCode.F2)
            || Input.GetKeyDown(KeyCode.F3)
            || Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("SelectTrack");
        }
    }
}
