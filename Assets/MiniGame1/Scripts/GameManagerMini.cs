using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//controls the flow of the game. Singleton so we can keep of highscores
public class GameManagerMini : MonoBehaviour
{
    public Player Player;
    public Timer Timer;
    public Spawner Spawner;
    public StartScreen StartScreen;
    public Score Score;
    public static GameManagerMini Instance { get; private set; } //game manager singleton
    public int highScore { get; set; } //highscore
    public int totalScore = 0; //total score to keep track of total points accumulated during mini game session. This resets when player quits game
    public int previousTotalScore = 0; //previous total score can be accessed for in game currency winnings from mini game

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
        Player = FindObjectOfType<Player>();
        Timer = FindObjectOfType<Timer>();
        Spawner = FindObjectOfType<Spawner>();
        StartScreen = FindObjectOfType<StartScreen>();
        Score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player == null)
        {
            AssignReferences();
        }

        if (SceneManager.GetActiveScene().name == "FallingMiniScene")
        {
            handleGame();
        }
    }

    public void handleGame()
    {
        //check if we are at the start screen
        if (!StartScreen.isStartScreen)
        {
            //check if we paused the game. If not, call the required function to run the game
            if (!StartScreen.isPaused)
            {
                //calling gameplay functions
                Player.Handle();
                Timer.Handle();
                Spawner.Handle();
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
            if (Timer.timer == 0)
            {
                StartScreen.gameOver = true;

                totalScore += Score.score; //update mini game total score during session
                Debug.Log("Total Session Score: " + totalScore);
                //for high score keeping
                if (highScore < Score.score)
                {
                    highScore = Score.score;

                    Debug.Log("NEW HIGHSCORE: " + highScore);
                    GameManager.Instance.coins += Score.score;
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
