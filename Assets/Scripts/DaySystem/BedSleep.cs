using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BedSleep : MonoBehaviour
{
    public Interact interact;
    public string[] newDayDialogue;
    private bool initialBedInteract = false;
    private GameObject bedInteract = null;
    public SaveLoadScript save;

    public Button yesButton;
    public Button noButton;
    //public Image image;

    bool yesPressed = false;

    public Camera mainCamera;

    void Start()
    {

        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        //image.gameObject.SetActive(false);
        yesButton.onClick.AddListener(handleButtonYes);
        noButton.onClick.AddListener(handleButtonNo);
    }

    // Update is called once per frame
    void Update()
    {
        bedInteract = null;

        if(initialBedInteract || DaySystem.instance.getDayNumber() > 1)
        {
            gameObject.GetComponent<TextHolder>().setDialogue(newDayDialogue);
            initialBedInteract = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bedInteract = interact.interactionTarget;

            if (initialBedInteract == false)
            {
                if (interact.interactionTarget != null)
                {
                    if (gameObject.GetComponent<TextHolder>().endOfIndex == true)
                    {
                        initialBedInteract = true;
                    }
                }
            }
            else if (initialBedInteract == true && gameObject.GetComponent<TextHolder>().endOfIndex == false)
            {
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(true);
            }
            /*
            else if (initialBedInteract == true)
            {
                if(GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().getPauseWorld() == true)
                {
                    yesButton.gameObject.SetActive(true);
                    noButton.gameObject.SetActive(true);
                }
            }*/
        }

        if(initialBedInteract == true)
        {
            if (gameObject.GetComponent<TextHolder>().endOfIndex == true && gameObject.GetComponent<InteractText>().getCompleteResetter() == false)
            {
                yesButton.gameObject.SetActive(false);
                noButton.gameObject.SetActive(false);
            }
        }

        

    }

    public void handleButtonYes()
    {
        yesButton.gameObject.SetActive(!yesButton.gameObject.activeSelf);
        noButton.gameObject.SetActive(!noButton.gameObject.activeSelf);
        if(DaySystem.instance == null)
        {
            Debug.Log("DAYSYS is NULL!!!");
        }
        DaySystem.instance.goodMorningDialogue();
        StartCoroutine(waitBeforeLoad());
        //image.gameObject.SetActive(true);
        //yesPressed = true;
    }

    private void handleButtonNo()
    {
        yesButton.gameObject.SetActive(!yesButton.gameObject.activeSelf);
        noButton.gameObject.SetActive(!noButton.gameObject.activeSelf);
        gameObject.GetComponent<TextHolder>().resetIndex();
        GameManager.Instance.getTextBox().GetComponent<TextBox>().closeBox();
        GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(false);
    }
    
    IEnumerator waitBeforeLoad()
    {
        save.SaveGame();
        yield return new WaitForSeconds(0.5f);
        DaySystem.instance.newDay = true;
        SceneManager.LoadScene("inside house");
    }
}
