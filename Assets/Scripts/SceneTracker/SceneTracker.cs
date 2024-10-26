using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class SceneTracker : MonoBehaviour
{
    public static SceneTracker Instance { get; private set; }
    public FadeScript fadeBackground;
    private string previousSceneName;
    // public AudioManager audioManager;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevents this object from being destroyed on scene loads
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance if it exists
            return;
        }

        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Initialize previous scene name
        previousSceneName = SceneManager.GetActiveScene().name;

        // Initial assignment of FadeScript
        reassignImage();
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when this object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method is called every time a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        reassignImage();

        if (scene.name == "TeamLogo")
        {
            logoToMenu();
        }
        else if (previousSceneName == "FallingMiniScene" && scene.name == "FallingMiniScene")
        {
            //do nothing, reloading mini game scene
            fadeBackground.gameObject.SetActive(false);
        }
        else if (scene.name != "Settings" && scene.name != "Inventory")
        {
            fadeBackground.StartFadeOutBlack();
        }

        //add more audio as needed
        if ((scene.name != previousSceneName && scene.name != "FallingMiniScene") && scene.name != "Settings" && (scene.name != "MainMenu" && previousSceneName != "Settings"))
        {
            AudioManager.instance.assignMusic();
        }
        //add other mini game exceptions here
        else if (previousSceneName == "FallingMiniScene" && scene.name == "critter quest")
        {
            //do nothing
        }

        // for money from minigame
        if (previousSceneName == "FallingMiniScene" && scene.name == "critter quest")
        {
            // Print the score from the Singleton
            Debug.Log("Money Earned From Mini Game: " + GameManagerMini.Instance.previousTotalScore);
        }
        Debug.Log(previousSceneName + " to " + scene.name);
        // Update the previous scene to the current one
        previousSceneName = scene.name;
    }

    //specific instructions from logo to main menu scene change
    private void logoToMenu()
    {
        //fadeBackground.StartFadeOutBlack();
        fadeBackground.StartSpecialLogoFade();
    }

    private void reassignImage()
    {
        fadeBackground = FindObjectOfType<FadeScript>();
    }
}
