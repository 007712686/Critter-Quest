using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsBehavior : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    private void Start()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRate;

        // Filter resolutions by refresh rate and aspect ratio (16:9)
        for (int i = 0; i < resolutions.Length; i++)
        {
            float aspectRatio = (float)resolutions[i].width / resolutions[i].height;
            if (resolutions[i].refreshRate == currentRefreshRate && Mathf.Approximately(aspectRatio, 16f / 9f))
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.ExclusiveFullScreen);
        Debug.Log(Screen.width + " " + Screen.height);
    }

    public void returnToStart()
    {
        Debug.Log("Back to start screen!");
        SceneManager.LoadScene("MainMenu");
    }

    public void manageResolution()
    {
        Debug.Log("Adjust resolution!");
        Debug.Log(Screen.width + "x" + Screen.height);
    }

    public void manageAudio()
    {
        Debug.Log("Adjust audio!");
    }
}
