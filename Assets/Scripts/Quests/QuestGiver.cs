using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestSO questGiven;
    // Start is called before the first frame update
    void Start()
    {
        if(DaySystem.instance != null)
        {
            questGiven = DaySystem.instance.currentQuest;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
