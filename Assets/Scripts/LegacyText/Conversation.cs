using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/New Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField]
    private List<DialogueLine> allLines;


    public void clearAllLines()
    {
        allLines.Clear();
    }
    public void setAllLines(DialogueLine y)
    {
        allLines.Add(y);
    }
    public DialogueLine GetLineByIndex(int index)
    {
        return allLines[index];
    }
    public int GetLength()
    {
        return allLines.Count - 1;
    }
}
