using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DaySystem : MonoBehaviour
{
    public static DaySystem instance;
    //public GameObject dayObject;
    public int dayNumber = 0;
    public int previousDayNumber = 0;
    public string[] newDialogue;
    public string[] mainDialogue;
    public bool newDay = true;
    public PetObject[] allPets;
    public QuestSO[] allQuests;
    public QuestSO currentQuest;
    public SaveLoadScript save;
    public bool isLoaded = false;
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
        if (SceneManager.GetActiveScene().name == "inside house")
        {

            if (DaySystem.instance.GetComponent<InteractText>() != null)
            {
                if (DaySystem.instance.getDayNumber() > 1 && DaySystem.instance.newDay == true)
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
                else if (DaySystem.instance.getDayNumber() > 1 && DaySystem.instance.newDay == false && DaySystem.instance.GetComponent<TextHolder>().endOfIndex == false)
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

            if (isLoaded)
            {
                dayNumber = 2;
                previousDayNumber = 2;
            }

            else if (newDay == true && dayNumber > 1)
            {
                previousDayNumber = dayNumber;
                //goodMorningDialogue();

            }
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
            if(isLoaded == true) 
            {
                goodMorningDialogue();
            }
            instance.GetComponent<TextHolder>().setDialogue(newDialogue);
            newDay = false;

        }
        isLoaded = false;
        questAssigner();

        if(dayNumber > 1)
        {
            Debug.Log("LOADINGHERE");
            //save.SaveGame();
            //StartCoroutine(WaitToLoad());
        }

        else if(dayNumber == 0) 
        {
            save.SaveGame();
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
        questAssigner();

        List<string> temp = new List<string>();

        temp.Add("Good Morning! Today is a new day!");

        for (int i = 0; i < allPets.Length; i++)
        {
            Debug.Log(allPets[i].name);
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
        Debug.Log(dayNumber);

        bool currentQuestCompleted = false;
        bool allQuestsCompleted = false;

        for (int i = 0; i < allQuests.Length; i++)
        {
            if (allQuests[i].questTurnedIn == true)
            {
                if (i == allQuests.Length - 1)
                {
                    allQuestsCompleted = true;
                }
            }
            else
            {
                allQuestsCompleted = false;
                break;
            }
        }

        //quest has been assigned already
        if (currentQuest != null)
        {
            if (currentQuest.questTurnedIn == true)
            {
                currentQuestCompleted = true;
            }
        }

        else if (dayNumber == 0)
        {
            currentQuestCompleted = true;
        }
        Debug.Log("All Quests Complete? - " + allQuestsCompleted.ToString());
        if (allQuestsCompleted == false && currentQuestCompleted == true)
        {
            Debug.Log("Assigning...");
            System.Random random = new System.Random();
            int randomIndex;

            do
            {
                randomIndex = random.Next(0, allQuests.Length);
            }

            while (allQuests[randomIndex].questTurnedIn == true);

            currentQuest = allQuests[randomIndex];
        }
    }

    public IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(1.5f);
        save.LoadGame();
    }

    public IEnumerator WaitToSave()
    {
        yield return new WaitForSeconds(0.75f);
        save.SaveGame();
    }

    public IEnumerator WaitToLoadUponWakingUp()
    {
        Debug.Log("Hello from coroutine!");
        while (!(instance.GetComponent<TextHolder>().endOfIndex && Input.GetKeyDown(KeyCode.Tab)))
        {
            yield return null;
        }
        Debug.Log("Goodbye from coroutine!");
        save.LoadGame();
    }

}
