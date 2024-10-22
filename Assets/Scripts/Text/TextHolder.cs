using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextHolder : MonoBehaviour
{
    [SerializeField]
    [TextArea(15, 20)]
    string[] dialogue;
    [SerializeField]
    int index = 0;
    public bool endOfIndex = true;
    private void Start()
    {
        
    }
    public int getIndex()
    {
        return index;
    }
    public string[] getDialogue()
    {
        return dialogue;
    }
    public void increaseIndex()
    {
        index++;
    }
    public void resetIndex()
    {
        index = 0;
        endOfIndex = true;
    }
    public void startConvo()
    {
        endOfIndex = false;
        if (this.gameObject.GetComponent<InteractText>().interrupt == false)
        {
            if (index < dialogue.Length)
            {
                this.gameObject.GetComponent<InteractText>().interactWith(dialogue[index]);
            }
        }
        else if(this.gameObject.GetComponent<InteractText>().interrupt == true)
        {
            print("Going");
            if(this.gameObject.GetComponent<QuestGiver>() != null)
            {
                GameManager.Instance.questManager.GetComponent<QuestTracker>().questInQuestion = this.gameObject.GetComponent<QuestGiver>().questGiven;
                GameManager.Instance.questManager.GetComponent<QuestAssigner>().questHolder.GetComponent<QuestBoard>().openBoard();
                GameManager.Instance.getTextBox().GetComponent<TextBox>().closeBox();
            }
        }
    }
    public void setDialogue(string[] newDialogue)
    {
        dialogue = newDialogue;
    }


}
