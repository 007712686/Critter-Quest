using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCreature : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        Debug.Log("Play Button Clicked!");
        GameManager.Instance.overPos = GameManager.Instance.getPlayer().transform.position;
        play();
    }
    public void play()
    {
        DaySystem.instance.save.SaveGame();
        PetInfo petName = GetComponentInParent<PetInfo>();
        if(petName.thisPet.petName == "Slime")
        {
            //Set up minigame here
            SceneManager.LoadScene("FallingMiniScene");
        }
        else if(petName.thisPet.petName == "Hellcat")
        {
            SceneManager.LoadScene("LaserMiniGame");
        }
        else if(petName.thisPet.petName == "Spirit Turtle")
        {
            SceneManager.LoadScene("SpiritHideNSeek");
        }

    }
}
