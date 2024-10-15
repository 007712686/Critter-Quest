using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class to keep track of start screen buttons
public class StartScreen : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject QuitButton;
    public GameObject ResumeButton;
    public bool isStartScreen;
    public bool isPaused;
    public bool gameOver = false;
    public bool isQuit = false;
    // Start is called before the first frame update

    //at the beginning of game, we set isStartScreen to true, and paused
    void Start()
    {
        isStartScreen = true;
        isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //start screen buttons to active
    public void displayStartScreen()
    {
        /*
        if (gameOver == true)
        {
            //reset the scene to start a new game. Game manager will reassign all gameobjects at the restart
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        */
        PlayButton.SetActive(true);
        QuitButton.SetActive(true);
    }

    //pause buttons
    public void pause()
    {
        ResumeButton.SetActive(true);
        QuitButton.SetActive(true);
    }

    //this gets me some logic if the game is being started. Note there are action in the Unity UI Inspector attached to the buttons
    public void startGame()
    {
        isQuit = false;
        isStartScreen = false;
        isPaused = false;
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
    }
    //resuming game
    public void resumeGame()
    {
        isPaused = false;
        ResumeButton.SetActive(false);
        QuitButton.SetActive(false);
    }

    //dont think we will end up doing a game reset cuz the game is so short anyway. 
    public void resetGame()
    {
        
    }

    //here we can quit the game and exit the entire scene.
    public void quitGame()
    {
        isQuit = true;
        //launch scene switching back to the world scene
        Debug.Log("QUIT");
    }
}
