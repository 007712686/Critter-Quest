using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeController : MonoBehaviour
{
    public QuestSO[] allQuests;
    public GameObject[] revealBadges;
    // Start is called before the first frame update
    void Start()
    {
        revealAfterQuestTurnedIn();
    }

    public void revealAfterQuestTurnedIn()
    {
        for (int i = 0; i < allQuests.Length; i++)
        {
            if (allQuests[i].questTurnedIn)
            {
                revealBadges[i].SetActive(true);
            }
            else
            {
                revealBadges[i].SetActive(false);
            }
        }
    }
}
