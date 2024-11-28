using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTurtle : MonoBehaviour
{
    public CurrentScore Score;
    public StartScreen StartScreen;
    public TurtleTime Time;
    public SpiritSpawner Spawner;
    public PlayerSearch PlayerSearch;
    public PlayerController PlayerController;
    public PlayerInteraction PlayerInteraction;
    public int totalScore = 0;
    public int previousTotalScore = 0; //previous total score can be accessed for in game currency winnings from mini game

    public static GameManagerTurtle Instance { get; private set; } //game manager singleton
    public int highScore { get; set; } //highscore
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevents the object from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject); //destroy duplicates of game manager (mini)
        }
    }

    private void Start()
    {
        //need to assign references just in case not found in the inspector
        AssignReferences();
    }
    //when the scene is enabled
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    //when the scene is disabled
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    //when the scene is reloaded. this gets called if OnEnabled is triggered or OnDisabled is triggered
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // reassign references after scene reload. We need this because otherwise, the gamemanager will be assigned to gameobjects that have been destroyed because of the scene reload
        AssignReferences();
    }

    //assigning references. Mainly this is needed for scene being reloaded
    private void AssignReferences()
    {
        Time = FindObjectOfType<TurtleTime>();
        StartScreen = FindObjectOfType<StartScreen>();
        Score = FindObjectOfType<CurrentScore>();
        //Spawner = FindObjectOfType<SpiritSpawner>();
        PlayerSearch = FindObjectOfType<PlayerSearch>();
        PlayerController = FindObjectOfType<PlayerController>();
        //PlayerInteraction = FindObjectOfType<PlayerInteraction>();
}

    // Update is called once per frame
    void Update()
    {
        if (Time == null)
        {
            AssignReferences();
        }

        if (SceneManager.GetActiveScene().name == "SpiritHideNSeek")
        {
            handleGame();
        }
    }

    private void handleGame()
    {
        if (!StartScreen.isStartScreen)
        {
            if (!StartScreen.isPaused)
            {
                Time.Handle(); //time
                //PlayerInteraction.Handler();
                PlayerSearch.Handler();
                PlayerController.Handler();

            }
            else
            {
                StartScreen.pause(); //pausing

                if (StartScreen.isQuit == true)
                {
                    onQuit();
                }
            }
            //if timer is set to 0, go to the gameover screen
            if (Time.remainingTime == 0)
            {
                StartScreen.gameOver = true;

                totalScore += Score.score; //update mini game total score during session
                Debug.Log("Total Session Score: " + totalScore);
                //for high score keeping
                if (highScore < Score.score)
                {
                    highScore = Score.score;

                    Debug.Log("NEW HIGHSCORE: " + highScore);
                }

                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //restarting scene!
                StartScreen.displayStartScreen(); //redisplay start screen
            }
        }
        if (StartScreen.isQuit == true)
        {
            onQuit();
        }
    }

    public void onQuit()
    {
        //previous total score can be accessed for in game currency winnings from mini game
        previousTotalScore = totalScore;

        //Enter code to send/save total score for in-game currency here
        totalScore = 0; //reset total mini game score only after making sure data transfers to next scene

        //insert code to switch scenes
        SceneManager.LoadScene("critter quest");
        GameManager.Instance.needReset = true;
    }
}
