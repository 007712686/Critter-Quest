using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "ScriptableObjects/Create Quest", order = 2)]
public class QuestSO : ScriptableObject
{
    public int questID, goal1ID, goal2ID, goal1Max, goal2Max, goal1Progress, goal2Progress;
    public string questName, questRequirements, questGoal1Words, questGoal2Words;
    public string questGiver;
    public bool questFinished, questTurnedIn, questAccepted;

}
