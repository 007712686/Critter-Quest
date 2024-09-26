using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInfo : MonoBehaviour
{
    //Holds info for the pet stats
    public float fullness;
    public float happiness;
    public bool healthy;
    public string species;
    public GameObject viewButton, petButton, playButton;
    // Start is called before the first frame update
    void Start()
    {
        if(species != null && happiness == 0)
        {
            if (species == "Slime")
            {
                GameManager.Instance.getSlimeStats(this.gameObject);
            }
            if (species == "Dog")
            {
                GameManager.Instance.getSecondStats(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
