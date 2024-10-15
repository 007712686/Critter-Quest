using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public List<QuestSO> currentQuests = new List<QuestSO>();
    public QuestSO questInQuestion;
    public void checkUpdatedInvenAdd(Item itemSent)
    {
        for (int i = 0; i < currentQuests.Count; i++)
        {
            if (itemSent.itemID == currentQuests[i].goal1ID)
            {
                print("Updated Quest: " + currentQuests[i].questName);
                currentQuests[i].goal1Progress++;
                if(currentQuests[i].goal1Progress >= currentQuests[i].goal1Max)
                {
                    print("Quest Goal 1 Complete!");
                }
            }
            else if (itemSent.itemID == currentQuests[i].goal2ID)
            {
                print("Updated Quest: " + currentQuests[i].questName);
                currentQuests[i].goal2Progress++;
                if (currentQuests[i].goal2Progress >= currentQuests[i].goal2Max)
                {
                    print("Quest Goal 2 Complete!");
                }
            }
            if(currentQuests[i].goal1Progress >= currentQuests[i].goal1Max && currentQuests[i].goal2Progress >= currentQuests[i].goal2Max)
            {
                print("Quest Complete!");
                currentQuests[i].questFinished = true;
                questInQuestion = null;
            }
        }
    }
    public void checkUpdatedInvenRemove(Item itemSent)
    {
        for (int i = 0; i < currentQuests.Count; i++)
        {
            //Code Here
        }
    }

    public void addQuest()
    {
        if (currentQuests.Contains(questInQuestion) != true && questInQuestion != null)
            currentQuests.Add(questInQuestion);
        else
            print("Major error, no double quests! Also no empty quests!");
        questInQuestion.questAccepted = true;
        this.gameObject.GetComponent<QuestAssigner>().questHolder.GetComponent<QuestBoard>().closeBoard();
    }
    public void denyQuest()
    {
        questInQuestion = null;
        this.gameObject.GetComponent<QuestAssigner>().questHolder.GetComponent<QuestBoard>().closeBoard();

    }

    public void turnInQuest()
    {
        questInQuestion.questTurnedIn = true;
        questInQuestion = null;
        currentQuests.Remove(questInQuestion);
        print("Put a reward here!");
        this.gameObject.GetComponent<QuestAssigner>().questHolder.GetComponent<QuestBoard>().closeBoard();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
