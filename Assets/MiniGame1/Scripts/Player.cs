using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public GameObject player;
    public StartScreen StartScreen;
    private Vector3 mousePosition;
    private float xBoundsLeft;
    private float xBoundsRight;
    public float setPadding;
    public float moveSpeed = 0.1f;

    // Use this for initialization
    void Start()
    {
        //set bounds of the player so he doesnt move out of bounds
        SetBounds();
    }

    // Update is called once per frame
    public void Handle()
    {
        //escape to pause. 
        if(Input.GetKeyUp(KeyCode.Escape) || StartScreen.isPaused == true)
        {
            //Launch Start Screen
            StartScreen.isPaused = true;
        }
        else
        {
            //not paused? move the player
            Move();
        }
    }

    private void Move()
    {
        //gather mouse input on relation to camera world
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.y = transform.position.y; //only move in the x direction, so make y = to it's original y position
        
        //keeping character within bounds. xBoundsLeft/Right is the furthest you can go on the screen
        if(mousePosition.x < xBoundsLeft)
        {
            mousePosition.x = xBoundsLeft;
        }
        if (mousePosition.x > xBoundsRight)
        {
            mousePosition.x = xBoundsRight;
        }
        //transforming the position
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

    //xBounds settings to force player movement in the screen
    private void SetBounds()
    {
        //note: padding is to "iron-out" the edges so we dont have the sprite full in the screen and not just partially within bounds.
        Camera gameCamera = Camera.main; //assign main camera to variable
        xBoundsLeft = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + setPadding; //positive direction 
        xBoundsRight = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - setPadding; //negative direction
    }
}
