using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class keeps track of game time!
public class Timer : MonoBehaviour
{
    public float timer = 5; //overall timer
    private float tempTime = 0; //this is to keep track for every 1 second past in order to update the Timer UI accordingly
    public Text timerText; //For the timer's text displayed in game

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Handle()
    {
        //Game over if timer hits 0
        if(timer > 0)
        {
            //if statement to keep track of every 1 second past for text UI
            if (tempTime > 1)
            {
                tempTime = 0; //temp time back to 0 to keep track of every 1 second past. 
                timer -= 1; //decrease timer by 1 exactly, otherwise timer will be inaccurate since we are wanting an integer
                timerText.text = "Time: " + timer.ToString(); //Display Timer in game
            }
            else
            {
                tempTime += Time.deltaTime; //update the temp timer only until it reaches 1 second (or greater)
            }
        }
    }
}
