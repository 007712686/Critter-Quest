using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    private string previousSceneName;
   // public AudioManager audioManager;

    void Awake()
    {
        //sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Store the initial scene name
        previousSceneName = SceneManager.GetActiveScene().name;

        // Don't destroy this object on scene loads
        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when this object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method is called every time a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(previousSceneName == "MainMenu" && scene.name == "critter quest")
        {
            AudioManager.instance.assignMusic();
        }
       
        // Check if the previous scene was the one you want to track
        if (previousSceneName == "FallingMiniScene" && scene.name == "critter quest")
        {
            // Print the score from the Singleton
            Debug.Log("Money Earned From Mini Game: " + GameManagerMini.Instance.previousTotalScore);
        }

        // Update the previous scene to the current one
        previousSceneName = scene.name;
    }
}
