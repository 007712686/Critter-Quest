using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public List<QuestSO> currentQuests = new List<QuestSO>();
    public QuestSO questInQuestion;
    public BadgeController badgeController;

    void Start()
    {
        
        
    }

    public void checkUpdatedInvenAdd(Item itemSent)
    {
        //Checks for quests that require an item, updates the inventory to see if the goals are met.
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
        //Checks if a quest is already taken.  Otherwise, adds the quest and checks if it was given via an NPC.  If so, return to dialogue.
        if (currentQuests.Contains(questInQuestion) != true && questInQuestion != null)
            currentQuests.Add(questInQuestion);
        else
            print("Major error, no double quests! Also no empty quests!");
        questInQuestion.questAccepted = true;
        this.gameObject.GetComponent<QuestAssigner>().questHolder.GetComponent<QuestBoard>().closeBoard();
        if (GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget != null)
        {
            if (GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<InteractText>() != null)
            {
                if (GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<InteractText>().interrupt == true)
                {
                    GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<InteractText>().interrupt = false;

                    GameManager.Instance.getTextBox().GetComponent<TextBox>().openBox();
                }
            }
        }
    }
    public void denyQuest()
    {
        questInQuestion = null;
        this.gameObject.GetComponent<QuestAssigner>().questHolder.GetComponent<QuestBoard>().closeBoard();
        if(GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget != null)
        {
            if (GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<InteractText>() != null)
            {
                if (GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<InteractText>().interrupt == true)
                {
                    GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<InteractText>().interrupt = false;

                    GameManager.Instance.getTextBox().GetComponent<TextBox>().openBox();
                }
            }
        }

    }

    public void turnInQuest()
    {
        questInQuestion.questTurnedIn = true;
        questInQuestion = null;
        currentQuests.Remove(questInQuestion);
        print("Put a reward here!");
        this.gameObject.GetComponent<QuestAssigner>().questHolder.GetComponent<QuestBoard>().closeBoard();
        if (GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget != null)
        {
            if ((GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<InteractText>() != null))
            {
                if (GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<InteractText>().interrupt == true)
                {
                    GameManager.Instance.getPlayer().GetComponent<Interact>().interactionTarget.GetComponent<InteractText>().interrupt = false;
                    GameManager.Instance.getTextBox().GetComponent<TextBox>().openBox();
                }
            }
        }
        badgeController.revealAfterQuestTurnedIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
