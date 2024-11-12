using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManagerLaser : MonoBehaviour
{
    // Start is called before the first frame update
    public RayScript RS;
    public LaserScript Laser;
    public CurrentScore Score;
    public Paw CatPaw;
    public StartScreen StartScreen;
    public Timer Time;
    public int totalScore = 0;
    public int previousTotalScore = 0; //previous total score can be accessed for in game currency winnings from mini game

    public static GameManagerLaser Instance { get; private set; } //game manager singleton
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
        RS = FindObjectOfType<RayScript>();
        Laser = FindObjectOfType<LaserScript>();
        Time = FindObjectOfType<Timer>();
        CatPaw = FindObjectOfType<Paw>();
        StartScreen = FindObjectOfType<StartScreen>();
        Score = FindObjectOfType<CurrentScore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RS == null)
        {
            AssignReferences();
        }

        if (SceneManager.GetActiveScene().name == "LaserMiniGame")
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
                RS.Handle(); //rayScript
                Time.Handle(); //time

                //detecting if raycast hit object
                if (RS.isHit == true)
                {
                    Score.addScore();

                    //call move on laser sprite
                    Laser.Move();
                    RS.isHit = false;
                }
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
            if (Time.timer == 0)
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

        Cursor.visible = true;  // bring back system cursor

        //insert code to switch scenes
        SceneManager.LoadScene("critter quest");
        GameManager.Instance.needReset = true;
    }
}
