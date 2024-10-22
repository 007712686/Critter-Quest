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
        if (index < dialogue.Length)
        {
            this.gameObject.GetComponent<InteractText>().interactWith(dialogue[index]);
        }
    }
    public void setDialogue(string[] newDialogue)
    {
        dialogue = newDialogue;
    }
}
