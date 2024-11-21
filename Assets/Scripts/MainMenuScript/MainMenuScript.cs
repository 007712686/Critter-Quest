using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public SaveLoadScript loadGame;
    public void enterNewGame()
    {
        Debug.Log("Entered new game!");
        SceneManager.LoadScene("inside house");
    }

    public void enterLoadGame()
    {
        Debug.Log("Enter load game!");
        //enter load screen via scenemanager here
        loadGame.LoadGame();
        DaySystem.instance.isLoaded = true;
        SceneManager.LoadScene("inside house");
    }

    public void enterSettings()
    {
        Debug.Log("Enter settings!");
        SceneManager.LoadScene("Settings");
    }

    // Update is called once per frame

}
