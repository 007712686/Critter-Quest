using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

//NOTE: THIS IS THE DESIGNATED PREFAB FOR THE FALLING PET SPRITE IN THE GAME. 
//Please see Spawner.cs to see how the prefab gets instantiated and spawned
public class FallingObjects : MonoBehaviour
{
    public float fallSpeed;
    public float xBoundsLeft;
    public float xBoundsRight;
    public float setPadding = 0.6f;
    private float randomXPos;

    public GameObject bucket;
    public Score score;
    public StartScreen screen;

    bool isCollided;

    // Start is called before the first frame update
    void Awake()
    {
        //setting the bounds of the screen so player doesnt wander off
        SetBounds();

        //finding related game objects
        bucket = GameObject.Find("Bucket");
        score = GameObject.Find("Score").GetComponent<Score>();
        screen = GameObject.Find("StartScreen").GetComponent<StartScreen>();

        //no collision to start
        isCollided = false;

        //random x position that is within bounds
        randomXPos = Random.Range(xBoundsLeft, xBoundsRight);
        transform.position = new Vector3(randomXPos, transform.position.y, transform.position.z); //setting position of the pet sprite randomly
    }

    // Update is called once per frame
    void Update()
    {
        //make sure screen isnt paused before entering sprite movement
        if (!screen.isPaused)
        {
            //making sprite fall from the sky to the ground
            transform.position = transform.position + (Vector3.down * fallSpeed) * Time.deltaTime;

            //collision check
            if (isCollided == true)
            {
                Debug.Log("Collided");
                Destroy(transform.root.gameObject); //destroy the sprite so we dont have excess gameobjects 
            }
            //ground collision. We want to check the buckets position otherwise the sprite will fall all the way off the map.
            if (bucket.transform.position.y > transform.position.y)
            {
                Debug.Log("HIT GROUND");
                Destroy(transform.root.gameObject); //destroy the sprite so we dont have excess gameobjects 
            }
        }
    }

    //function that sets the bounds of the screen
    private void SetBounds()
    {
        Camera gameCamera = Camera.main; //assign main camera to variable
        xBoundsLeft = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + setPadding;
        xBoundsRight = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - setPadding;
    }

    //collision checking and adding score. Only checking collision with the bucket for simplicity sake
    private void OnCollisionEnter2D(Collision2D collision)
    {
        score.addScore();
        isCollided = true;
    }

}
