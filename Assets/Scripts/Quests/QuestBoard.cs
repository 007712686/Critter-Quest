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
        acceptButton.GetComponentInChildren<Text>().text = "Accept";
        this.gameObject.transform.localPosition = new Vector2(-57, 20);
        turnInButton.transform.localPosition = new Vector3(10000, -10000, 0);
        acceptButton.transform.localPosition = new Vector3(0, -55.1f, 0);
        denyButton.transform.localPosition = new Vector3(0, -62.3f, 0);
        if (this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion != null)
        {
            qName.text = this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questName + " (" +
                    this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questID + ")";
            qReq.text = this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questRequirements;
            qg1.text = this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questGoal1Words;
            qg2.text = this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questGoal2Words;
            if (this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questAccepted == true)
            {
                acceptButton.GetComponentInChildren<Text>().text = "Close";
                denyButton.transform.localPosition = new Vector3(10000, -10000, 0);
            }
            if (this.gameObject.transform.parent.gameObject.GetComponent<QuestTracker>().questInQuestion.questFinished == true)
            {
                turnInButton.transform.localPosition = new Vector3(0, -55.1f, 0);
                acceptButton.transform.localPosition = new Vector3(10000, -10000, 0);

            }
        }


    }
    public void closeBoard()
    {
        this.gameObject.transform.localPosition = new Vector2(1000, -1000);
    }

}
