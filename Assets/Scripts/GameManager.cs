using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    string playerName;
    GameObject player;
    GameObject textBox;
    [SerializeField]
    GameObject cursor;
    [SerializeField]
    GameObject TextManager;
    [SerializeField]
    public GameObject inventory;


    public float slimeHappiness = 100, slimeFullness = 100;
    public bool slimeHealthy = true;
    public string slimeSpecies = "Slime";

    public float dogHappiness = 100, dogFullness = 100;
    public bool dogHealthy = true;
    public string dogSpecies = "Dog";
    protected GameManager()
    {

    }
    //Getters and Setters for each object that must accessable be in every scene
    public GameObject getTextManager()
    {
        return TextManager;
    }
    public void setTextManager(GameObject x)
    {
        TextManager = x;
    }
    public GameObject getCursor()
    {
        return cursor;
    }
    public void setCursor(GameObject x)
    {
        cursor = x;
    }
    public GameObject getPlayer()
    {
        return player;
    }
    public void setPlayer(GameObject x)
    {
        player = x;
    }
    public void setTextBox(GameObject x)
    {
        textBox = x;
    }
    public GameObject getTextBox()
    {
        return textBox;
    }
    //Getters and setters for each pet stats
    public void getSlimeStats(GameObject x)
    {
        x.GetComponent<PetInfo>().species = slimeSpecies;
        x.GetComponent<PetInfo>().fullness = slimeFullness;
        x.GetComponent<PetInfo>().happiness = slimeHappiness;
        x.GetComponent<PetInfo>().healthy = slimeHealthy;
    }
    public void setSlimeStats(float f, float h, bool e)
    {
        slimeSpecies = "Slime";
        slimeFullness = f;
        slimeHappiness = h;
        slimeHealthy = e;
    }
    public void getSecondStats(GameObject x)
    {
        x.GetComponent<PetInfo>().species = dogSpecies;
        x.GetComponent<PetInfo>().fullness = dogFullness;
        x.GetComponent<PetInfo>().happiness = dogHappiness;
        x.GetComponent<PetInfo>().healthy = dogHealthy;
    }
    public void setSecondStats(float f, float h, bool e)
    {
        slimeSpecies = "Dog";
        slimeFullness = f;
        slimeHappiness = h;
        slimeHealthy = e;
    }
    //Openers for inventory 
    public void openInventory()
    {
        inventory.SetActive(true);
    }
    public void closeInventory()
    {
        inventory.SetActive(false);
    }

    void Update()
    {
        //Escape while the player isnt paused
        if (Input.GetKeyDown(KeyCode.Backspace) && GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().getPauseWorld() == false)
        {
            GameManager.Instance.openInventory();
            GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(true);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace) && GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().getPauseWorld() == true)
        {
            GameManager.Instance.closeInventory();
            GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(false);

        }
    }
}
