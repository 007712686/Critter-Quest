using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestBoard : MonoBehaviour
{
    public bool turningInQuest = false;
    public Text qName, qReq, qg1, qg2;
    public GameObject turnInButton, acceptButton, denyButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            openBoard();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            closeBoard();
        }
    }

    public void openBoard()
    {
        //Opens up the board and sets all reference buttons to their proper values based on quest status
        acceptButton.GetComponentInChildren<Text>().text = "Accept";
        this.gameObject.transform.localPosition = new Vector2(37.5f, 20); //koda
        turnInButton.transform.localPosition = new Vector3(10000, -10000, 0);
        acceptButton.transform.localPosition = new Vector3(-95, -119.34f, 0); //koda
        denyButton.transform.localPosition = new Vector3(93.7f, -119.34f, 0); //koda
        if (this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion != null)
        {
            qName.text = this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questName;
            //qName.text = this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questName + " (" +
            //this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questID + ")";
            qReq.text = this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questRequirements;
            qg1.text = this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questGoal1Words;
            qg2.text = this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questGoal2Words;
            if (this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questAccepted == true)
            {
                acceptButton.GetComponentInChildren<Text>().text = "Close";
                acceptButton.transform.localPosition = new Vector3(1.58f, -119.34f, 0); //koda
                denyButton.transform.localPosition = new Vector3(10000, -10000, 0);
            }
            if (this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questFinished == true)
            {
                turnInButton.transform.localPosition = new Vector3(1.58f, -119.34f, 0); //koda
                acceptButton.transform.localPosition = new Vector3(10000, -10000, 0);

            }
        }


    }
    public void closeBoard()
    {
        this.gameObject.transform.localPosition = new Vector2(1000, -1000);
    }

}
