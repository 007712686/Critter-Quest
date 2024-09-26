using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public Speaker speaker;
    [TextArea]
    public string[] dialogue;
}