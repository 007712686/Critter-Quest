using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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

        //player pos resets
        if(scene.name == "critter quest" && previousSceneName != "inside house")
        {
            loadPlayerPos();
        }
        else if(scene.name == "inside house" && previousSceneName != scene.name && previousSceneName != "MainMenu")
        {
            StartCoroutine(waitToMovePlayer());
        }

        //reassigning dayinstance text for dialogue AND waiting to load game for data
        if(scene.name == "inside house")
        {
            reassignDayText();
            //loading upon entering inside house scene
            if(DaySystem.instance != null && previousSceneName != "MainMenu" && previousSceneName != "inside house")
            {
                StartCoroutine(DaySystem.instance.WaitToLoad());
            }
            else if (DaySystem.instance != null && DaySystem.instance.getDayNumber() > 0)
            {
                StartCoroutine(DaySystem.instance.WaitToLoadUponWakingUp());
            }
            else
            {
                
            }
        }

        //reassigning the save object for day system instance
        else if(scene.name == "critter quest" || scene.name == "store")
        {
            reassignDaySave();
            if(previousSceneName == "FallingMiniScene" || previousSceneName == "LaserMiniGame")
            {
                //StartCoroutine(DaySystem.instance.WaitToSave());
            }
            if (DaySystem.instance != null)
            {
                StartCoroutine(DaySystem.instance.WaitToLoad());
            }
        }

        //reassigning quest object for other scenes
        if(scene.name == "critter quest" || scene.name == "store")
        {
            reassignQuest();
        }

        if (scene.name == "TeamLogo")
        {
            logoToMenu();
        }
        else if ((previousSceneName == "FallingMiniScene" && scene.name == "FallingMiniScene") || (previousSceneName == "LaserMiniGame" && scene.name == "LaserMiniGame"))
        {
            //do nothing, reloading mini game scene
            fadeBackground.gameObject.SetActive(false);
        }
        else if (scene.name != "Settings" && scene.name != "Inventory")
        {
            fadeBackground.StartFadeOutBlack();
        }

        //add more audio as needed
        if ((scene.name != previousSceneName && scene.name != "FallingMiniScene" && scene.name != "LaserMiniGame") && (previousSceneName != "FallingMiniScene" && previousSceneName != "LaserMiniGame") && scene.name != "Settings" && (scene.name != "MainMenu" && previousSceneName != "Settings"))
        {
            AudioManager.instance.assignMusic();
        }
        //add other mini game exceptions here
        else if ((previousSceneName == "FallingMiniScene" || previousSceneName == "LaserMiniGame") && scene.name == "critter quest")
        {
            //do nothing
        }

        // for money from minigame
        if (previousSceneName == "FallingMiniScene" && scene.name == "critter quest")
        {
            StartCoroutine(saveCoinsAfterLoad(GameManagerMini.Instance.previousTotalScore));
            //GameManager.Instance.coins += GameManagerMini.Instance.previousTotalScore;
            // Print the score from the Singleton
            Debug.Log("Money Earned From Mini Game: " + GameManagerMini.Instance.previousTotalScore);
        }
        else if (previousSceneName == "LaserMiniGame" && scene.name == "critter quest")
        {
            StartCoroutine(saveCoinsAfterLoad(GameManagerLaser.Instance.previousTotalScore));
            //GameManager.Instance.coins += GameManagerLaser.Instance.previousTotalScore;
            // Print the score from the Singleton
            Debug.Log("Money Earned From Mini Game: " + GameManagerLaser.Instance.previousTotalScore);
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

    private void reassignDayText()
    {
        DaySystem.instance.GetComponent<InteractText>().textField = GameObject.Find("TextHouse").GetComponent<Text>();
        DaySystem.instance.GetComponent<DaySystem>().save = GameObject.Find("SaveLoad").GetComponent<SaveLoadScript>();
        if (DaySystem.instance.getDayNumber() > 0 && DaySystem.instance.newDay == true && DaySystem.instance.isLoaded == false)
        {
            DaySystem.instance.endDay();
        }
    }

    private void reassignDaySave()
    {
        if(DaySystem.instance != null)
        {
            DaySystem.instance.GetComponent<DaySystem>().save = GameObject.Find("SaveLoad").GetComponent<SaveLoadScript>();
        }
       
    }

    private void reassignQuest()
    {
        Debug.Log("QUEST MANAGER ASSIGNMENT");
        GameManager.Instance.questManager = FindObjectOfType<QuestTracker>().gameObject;

        if (DaySystem.instance != null)
        {
            if (DaySystem.instance.currentQuest.questAccepted)
            {
                
                GameManager.Instance.questManager.GetComponent<QuestTracker>().questInQuestion = DaySystem.instance.currentQuest;
                GameManager.Instance.questManager.GetComponent<QuestTracker>().currentQuests.Add(DaySystem.instance.currentQuest);
            }
        }
    }

    private void loadPlayerPos()
    {
        GameManager.Instance.needReset = true;
    }

    private IEnumerator saveCoinsAfterLoad(int coins)
    {
        yield return new WaitForSeconds(1f);
        DaySystem.instance.save.LoadGame();
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.coins += coins;
        yield return new WaitForSeconds(1f);
        DaySystem.instance.save.SaveGame();

    }

    private IEnumerator waitToMovePlayer()
    {
        yield return new WaitForSeconds(0.25f);
        if (GameManager.Instance.player != null)
        {
            GameManager.Instance.player.transform.position = new Vector3(-1, -4, 0);
        }
    }
}
