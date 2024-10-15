using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fallingObjects; //prefab!
    public float spawnRate = 2; //initial spawnRate (every 2 seconds... this will slowly decrease as the game goes on)
    private float decreaseSpawnRate = 0.1f; //decreasing the spawnRate by 0.1 seconds progressively
    private float timer = 0; //just a temp timer

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Handle()
    {
        //here we are adding timer as long as its less than the spawnRate. The goal is to progressively descrease spawnRate.
        if(timer < spawnRate)
        {
            timer = timer + Time.deltaTime; //counting time
        }
        else
        {
            //resetting timer
            timer = 0;
            //here we have a limit of 0.5 seconds for the spawn rate (any less, the game is too difficult
            if(spawnRate > 0.5)
            {
                //spawnRate will decrease by -decreaseSpawnRate. 
                spawnRate -= decreaseSpawnRate;
            }
            //instantiating prefab fallingObject
            Instantiate(fallingObjects, transform.position, transform.rotation);
        }
    }
}
