using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue; //Used to hold text info
    public GameObject dialogueBox;
    //public Image speakerSprite;
    [SerializeField]
    private int currentIndex;
    private static DialogueManager instance;
    private Conversation currentConvo;
    //private Animator anim;
    private Coroutine typing;
    [SerializeField]
    private bool finishedBox = true;


    public GameObject signPostTest;
    public bool getFinishedBox()
    {
        return finishedBox;
    }
    public int getCurrentIndex()
    {
        return currentIndex;
    }
    private void Awake() //Used to detect if an instance of the box exists, destroys it, and replaces it
    {
        if (instance == null)
        {
            instance = this;
            //anim = dialogueBox.GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void StartConversation(Conversation convo) //Opens text box, resets values, and reads the next line
    {
        //instance.anim.SetBool("isOpen", true);
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";
        instance.currentIndex = 0;
        instance.ReadNext();
    }

    public void ReadNext()
    {
        if (finishedBox == true) //Make sure this line of text is done before doing the next
        {
            if (currentIndex > (currentConvo.GetLength())) //Checks if all lines have been read and closes the box
            {
                //instance.anim.SetBool("isOpen", false);
                return;
            }
            //if (currentConvo.GetLineByIndex(currentIndex).speaker != null) //Used to prevent issue in battle where there are no speakers
                //speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName(); //Set the speaker to the proper one listed for the text
            //else
                //speakerName.text = "";
            if (typing == null)
            {
                typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));  //Used to producing type writer effect
            }
            else if (finishedBox == true)
            {
                instance.StopCoroutine(typing); //Stops the typing look
                typing = null; //Resets the value
                typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue)); //Starts the next line of typing
            }
            //if (currentConvo.GetLineByIndex(currentIndex).speaker != null) //Same as with the speaker's name, but with their sprite
                //speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
            //else
                //speakerSprite.GetComponent<Image>().color = new Color32(0, 0, 0, 0);


            currentIndex++;//Increases the index of the lines, moving on once one has finished
        }

    }

    private IEnumerator TypeText(string[] text) //Types the text on screen
    {
        for (int i = 0; i < text.Length;i++)
        {
            if (text[0].Length >= signPostTest.GetComponent<InteractText>().maxCharSize)
            {
                print(text[0] + " will not fit!!!");
            }
            else
            {
                bool complete = false; //Checks if the text has been completed
                int index = 0;
                while (!complete)
                {
                    dialogue.text += text[index];
                    index++;
                    yield return new WaitForSeconds(.1f);

                    if (index == text.Length)
                    {
                        complete = true;
                    }
                }
            }
                typing = null;
                finishedBox = true;
            
        }
    }
}
