using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractText : MonoBehaviour
{
    [SerializeField]
    string[] currentTextArray;
    [SerializeField]
    public int maxCharSize;
    [SerializeField]
    char currentChar;
    [SerializeField]
    Text textField;
    [SerializeField]
    string currentDisplayText;
    [SerializeField]
    int charsLeft;
    [SerializeField]
    bool endOfBox = false;
    [SerializeField]
    private bool isTyping = false;
    [SerializeField]
    KeyCode interact;
    [SerializeField]
    int numOfNewLines = 0;
    [SerializeField]
    bool complete = false;
    [SerializeField]
    int index = 0;
    [SerializeField]
    bool completeResetter = false;
    [SerializeField]
    bool firstBoxBroughtUp = false;
    private void LateUpdate()
    {
        if (Input.GetKeyDown(interact) == true)
        {
            if (endOfBox == true)
            {
                if (index < currentDisplayText.Length && completeResetter != true)
                {
                    complete = false;
                    endOfBox = false;
                    numOfNewLines = 0;
                    StartCoroutine(TypeText(currentDisplayText, index));
                }
            }
            if (completeResetter == true)
            {
                textField.text = "";
                currentDisplayText = "";
                completeResetter = false;
                complete = false;
                firstBoxBroughtUp = false;
                GameManager.Instance.getTextBox().GetComponent<TextBox>().closeBox();
                StartCoroutine("ResumeMovement");
            }
        }
    }
    //USED TO RESUME WORLD MOVEMENT
    IEnumerator ResumeMovement()
    {
        yield return new WaitForSeconds(.3f);
        GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(false);
        StopCoroutine("ResumeMovement");
    }

    public void interactWith(string dialogue)
    {
        currentDisplayText = "";
        splitString(dialogue);
        readText(currentTextArray);
        StartCoroutine(TypeText(currentDisplayText, index));
        GameManager.Instance.getTextBox().GetComponent<TextBox>().openBox();
        GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(true);
    }

    void splitString(string currentText)
    {
        currentTextArray = currentText.Split(' ');
    }
    void readText(string[] dialogue)
    {
        charsLeft = maxCharSize;
        for (int i = 0; i < dialogue.Length; i++)
        {
            if (dialogue[i].Length >= charsLeft)
            {
                currentDisplayText = currentDisplayText + '\n' + dialogue[i] + " ";
                charsLeft = maxCharSize - (dialogue[i].Length + 1);
            }
            else
            {
                charsLeft -= dialogue[i].Length + 1;
                currentDisplayText = currentDisplayText + dialogue[i] + " ";
            }
        }
    }

    //This function handles the opening of a text box, pausing the world, and type-writer text animations
    //for text boxes.  Inspector inputted strings are broken up and displayed with user input influencing 
    //new text boxes opening.  This code also breaks up text to fit within confined boxes.
    public IEnumerator TypeText(string text, int ind) //Types the text on screen
    {
        textField.text = "";
        isTyping = true;
        if (firstBoxBroughtUp == false)
        {
            yield return new WaitForSeconds(.3f);
            firstBoxBroughtUp = true;
        }
        while (!complete)
        {
            if (ind < text.Length)
            {
                textField.text += text[ind];
            }
            if (textField.text[0] == '\n')
            {
                textField.text = "";
                numOfNewLines -= 1;
            }
            if (ind < text.Length && text[ind] == '\n')
            {
                numOfNewLines += 1;
            }
            if (numOfNewLines < 2)
            {
                yield return new WaitForSeconds(.01f);

                if (ind >= text.Length)
                {

                    if (this.gameObject.GetComponent<TextHolder>().getIndex() >= this.gameObject.GetComponent<TextHolder>().getDialogue().Length - 1)
                    {
                        complete = true;
                        endOfBox = false;
                        setIndex(0);
                        isTyping = false;
                        numOfNewLines = 0;
                        completeResetter = true;
                        StopCoroutine("TypeText");
                        this.gameObject.GetComponent<TextHolder>().resetIndex();
                        break;
                    }
                    else
                    {
                        this.gameObject.GetComponent<TextHolder>().increaseIndex();
                        numOfNewLines = 0;
                        break;
                    }
                }
                ind++;
            }
            else
            if (numOfNewLines >= 2)
            {
                setIndex(ind);
                endOfBox = true;
                complete = true;
            }
        }
        isTyping = false;
        StopCoroutine("TypeText");
    }

    public char getCurrentChar()
    {
        return currentChar;
    }
    public bool getCompleteResetter()
    {
        return completeResetter;
    }
    public bool getEndOfBox()
    {
        return endOfBox;
    }
    public bool getIsTyping()
    {
        return isTyping;
    }

    public void setIndex(int x)
    {
        index = x;
    }
}
