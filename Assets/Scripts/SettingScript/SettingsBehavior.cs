using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsBehavior : MonoBehaviour
{
    public void returnToStart()
    {
        Debug.Log("Back to start screen!");
        SceneManager.LoadScene("MainMenu");
    }

    public void manageResolution()
    {
        Debug.Log("Adjust resolution!");
    }

    public void manageAudio()
    {
        Debug.Log("Adjust audio!");
    }
}
