using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : MonoBehaviour
{
    public static DaySystem instance;
    public GameObject dayObject;
    public static int dayNumber = 0;
    public string[] newDialogue;
    public bool newDay = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        day1Dialogue();
    }

    public void endDay()
    {

        dayNumber++;
    }

    public void showEndOfDayStats()
    {

    }

    public void goodMorning()
    {
        if(dayNumber == 0)
        {
            day1Dialogue();
        }

        else
        {
            goodMorningDialogue();
        }
    }

    public void day1Dialogue()
    {
        
        if(newDay == true)
        {
            instance.GetComponent<TextHolder>().setDialogue(newDialogue);
            newDay = false;
        }
        
        instance.GetComponent<TextHolder>().startConvo();
    }

    public void goodMorningDialogue()
    {
        
    }

    public int getDayNumber()
    {
        return dayNumber;
    }

    public void dayStartConvo()
    {
        
    }

}
