using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractText : MonoBehaviour
{
    // Array to hold the current text split into words
    [SerializeField]
    string[] currentTextArray;

    // Maximum number of characters allowed in a line
    [SerializeField]
    public int maxCharSize;

    // Character currently being processed
    [SerializeField]
    char currentChar;

    // UI Text field to display the dialogue
    [SerializeField]
    public Text textField;

    // Full text to be displayed in the text box
    [SerializeField]
    string currentDisplayText;

    // Remaining characters that can be displayed in the current line
    [SerializeField]
    int charsLeft;

    // Flag to indicate if the text box has reached the end
    [SerializeField]
    bool endOfBox = false;

    // Indicates if the text is currently being typed out
    [SerializeField]
    private bool isTyping = false;

    // Key to interact with the text box
    [SerializeField]
    public KeyCode interact;

    // Number of new lines encountered in the text
    [SerializeField]
    int numOfNewLines = 0;

    // Indicates if the text display is complete
    [SerializeField]
    bool complete = false;

    // Index of the current character being displayed
    [SerializeField]
    int index = 0;

    // Flag to reset the complete state
    [SerializeField]
    bool completeResetter = false;

    // Indicates if the first text box has been opened
    [SerializeField]
    bool firstBoxBroughtUp = false;

    [SerializeField]
    public bool interrupt = false;

    // Update method called once per frame
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K) == true)
        {
            interrupt = false;
            print("Resume Convo");

        }
        if (Input.GetKeyDown(KeyCode.J) == true)
        {
            interrupt = true;
            print("Interrupted Convo");

        }
        // Check if the interact key was pressed
        if (Input.GetKeyDown(interact) == true)
        {
            // If the text box has reached the end
            if (endOfBox == true)
            {
                // Continue displaying text if not complete
                if (index < currentDisplayText.Length && completeResetter != true)
                {
                    complete = false; // Reset complete state
                    endOfBox = false; // Prepare for new text
                    numOfNewLines = 0; // Reset new lines count
                    StartCoroutine(TypeText(currentDisplayText, index)); // Start typing the text
                }
            }
            // Reset the text box and state if completeResetter is true
            if (completeResetter == true)
            {
                textField.text = ""; // Clear text field
                currentDisplayText = ""; // Clear current display text
                completeResetter = false; // Reset complete reset state
                complete = false; // Reset complete state
                firstBoxBroughtUp = false; // Reset first box flag
                GameManager.Instance.getTextBox().GetComponent<TextBox>().closeBox(); // Close the text box
                StartCoroutine("ResumeMovement"); // Resume player movement
            }
        }
    }

    // Coroutine to resume player movement after a short delay
    IEnumerator ResumeMovement()
    {
        yield return new WaitForSeconds(.3f); // Wait for 0.3 seconds
        GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(false); // Unpause the world
        StopCoroutine("ResumeMovement"); // Stop this coroutine
    }

    // Method to start interacting with a new dialogue
    public void interactWith(string dialogue)
    {
        currentDisplayText = ""; // Clear the current display text
        splitString(dialogue); // Split the dialogue into words
        readText(currentTextArray); // Prepare the text for display
        StartCoroutine(TypeText(currentDisplayText, index)); // Start typing the text
        GameManager.Instance.getTextBox().GetComponent<TextBox>().openBox(); // Open the text box
        GameManager.Instance.getPlayer().GetComponent<PlayerMovement>().setPauseWorld(true); // Pause the world
    }

    // Method to split a string into an array of words
    void splitString(string currentText)
    {
        currentTextArray = currentText.Split(' '); // Split the input text by spaces
    }

    // Method to read and format the text for display
    void readText(string[] dialogue)
    {
        charsLeft = maxCharSize; // Initialize remaining characters to max size
        for (int i = 0; i < dialogue.Length; i++)
        {
            // If the word is too long for the current line
            if (dialogue[i].Length >= charsLeft)
            {
                currentDisplayText = currentDisplayText + '\n' + dialogue[i] + " "; // Start a new line
                charsLeft = maxCharSize - (dialogue[i].Length + 1); // Reset chars left
            }
            else
            {
                charsLeft -= dialogue[i].Length + 1; // Update chars left
                currentDisplayText = currentDisplayText + dialogue[i] + " "; // Add the word to current text
            }
        }
    }

    // Coroutine that handles typing animation for text display
    public IEnumerator TypeText(string text, int ind) // Types the text on screen
    {
        textField.text = ""; // Clear the text field
        isTyping = true; // Set typing state to true
        if (firstBoxBroughtUp == false) // Check if this is the first box
        {
            yield return new WaitForSeconds(.3f); // Wait briefly before typing
            firstBoxBroughtUp = true; // Set flag to true
        }
        // Continue typing until the text is complete
        while (!complete)
        {
            // If there are still characters to display
            if (ind < text.Length)
            {
                textField.text += text[ind]; // Display the next character
            }
            // If the first character is a newline, reset the text field
            if (textField.text[0] == '\n')
            {
                textField.text = ""; // Clear text field
                numOfNewLines -= 1; // Decrease new lines count
            }
            // Check for new line characters in the text
            if (ind < text.Length && text[ind] == '\n')
            {
                numOfNewLines += 1; // Increase new lines count
            }
            // If the number of new lines is less than 2
            if (numOfNewLines < 2)
            {
                // If all characters have been displayed
                if (ind >= text.Length)
                {
                    // If we have reached the end of the dialogue
                    if (this.gameObject.GetComponent<TextHolder>().getIndex() >= this.gameObject.GetComponent<TextHolder>().getDialogue().Length - 1)
                    {
                        complete = true; // Mark as complete
                        endOfBox = false; // Reset end of box flag
                        setIndex(0); // Reset index
                        isTyping = false; // Reset typing state
                        numOfNewLines = 0; // Reset new lines count
                        completeResetter = true; // Trigger reset state
                        StopCoroutine("TypeText"); // Stop this coroutine
                        this.gameObject.GetComponent<TextHolder>().resetIndex(); // Reset text holder index
                        break; // Exit loop
                    }
                    else
                    {
                        this.gameObject.GetComponent<TextHolder>().increaseIndex(); // Move to the next dialogue
                        numOfNewLines = 0; // Reset new lines count
                        setIndex(0); // Reset index

                        break; // Exit loop
                    }
                }
            }
            else
            // If the number of new lines is greater than or equal to 2
            if (numOfNewLines >= 2)
            {
                setIndex(ind); // Set the current index
                endOfBox = true; // Set end of box flag
                complete = true; // Mark typing as complete
            }
            if (endOfBox != true)
            {
                yield return new WaitForSeconds(.01f); // Wait briefly before showing the next character
                ind++; // Move to the next character
            }
        }
        isTyping = false; // Reset typing state
        StopCoroutine("TypeText"); // Stop this coroutine
    }

    // Method to get the current character being typed
    public char getCurrentChar()
    {
        return currentChar; // Return current character
    }

    // Method to check if the reset state is active
    public bool getCompleteResetter()
    {
        return completeResetter; // Return complete reset state
    }

    // Method to check if the text box has reached the end
    public bool getEndOfBox()
    {
        return endOfBox; // Return end of box state
    }

    // Method to check if text is currently being typed
    public bool getIsTyping()
    {
        return isTyping; // Return typing state
    }

    // Method to set the current index for typing
    public void setIndex(int x)
    {
        index = x; // Set index
    }
}
