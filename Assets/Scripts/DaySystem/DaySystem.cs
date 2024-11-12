using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : MonoBehaviour
{
    public static DaySystem instance;
    //public GameObject dayObject;
    public static int dayNumber = 0;
    public int previousDayNumber = 0;
    public string[] newDialogue;
    public string[] mainDialogue;
    public bool newDay = true;
    public PetObject[] allPets;
    public QuestSO[] allQuests;
    public QuestSO currentQuest;
 
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
    }

    private void Update()
    {
        if (DaySystem.instance.GetComponent<InteractText>() != null)
        {
            if (DaySystem.instance.getDayNumber() > 1 &&  DaySystem.instance.newDay == true)
            {
                if (DaySystem.instance.GetComponent<InteractText>().getIsTyping() == false)
                {
                    
                    if (DaySystem.instance.newDay == true && previousDayNumber == dayNumber)
                    {
                        
                        GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(true);
                        DaySystem.instance.day1Dialogue();
                    }
                    else if (Input.GetKeyDown(KeyCode.Tab))
                    {
                        DaySystem.instance.day1Dialogue();
                    }
                }
            }
            else if(DaySystem.instance.getDayNumber() > 1 && DaySystem.instance.newDay == false && DaySystem.instance.GetComponent<TextHolder>().endOfIndex == false)
            {
                if (DaySystem.instance.GetComponent<InteractText>().getIsTyping() == false)
                {
                    if (Input.GetKeyDown(KeyCode.Tab))
                    {
                        DaySystem.instance.day1Dialogue();
                    }
                }
            }
        }

        if(newDay == true && dayNumber > 1)
        {
            previousDayNumber = dayNumber;
            //goodMorningDialogue();

        }
    }

    public void endDay()
    {
        dayNumber++;
        //newDay = true;
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

    public void afterDay1Dialogue()
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
        List<string> temp = new List<string>();

        temp.Add("Good Morning! Today is a new day!");

        for(int i = 0; i < allPets.Length; i++)
        {
            if (allPets[i].happiness >= 100 && allPets[i].fullness >= 100)
            {
                allPets[i].level++;
                allPets[i].happiness = 0;
                allPets[i].fullness = 0;
                temp.Add(allPets[i].name + " has increased to level " + allPets[i].level + "!!!");
            }
        }

        newDialogue = temp.ToArray();
    }

    public int getDayNumber()
    {
        return dayNumber;
    }

    public void questAssigner()
    {
        bool allQuestsCompleted = false;

        if(currentQuest != null)
        {
            if(currentQuest.questTurnedIn == true)
            {
                for (int i = 0; i < allQuests.Length; i++)
                {
                    if (allQuests[i].questTurnedIn == true)
                    {
                        if (i == allQuests.Length)
                        {
                            allQuestsCompleted = true;
                        }
                    }
                }
                if (allQuestsCompleted == false)
                {
                    System.Random random = new System.Random();
                    int randomIndex;

                    do
                    {
                        randomIndex = random.Next(0, allQuests.Length - 1);
                    }

                    while (allQuests[randomIndex].questTurnedIn == true);

                    currentQuest = allQuests[randomIndex];
                }
            }
            else
            {
                //keep current quest
            }
        }
    }

}
