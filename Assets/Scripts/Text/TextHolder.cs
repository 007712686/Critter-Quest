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
    }
    public void startConvo()
    {
        if (index < dialogue.Length)
        {
            this.gameObject.GetComponent<InteractText>().interactWith(dialogue[index]);
        }
    }
}
