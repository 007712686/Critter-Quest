using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Choice", menuName = "Dialogue/Choice")]
public class ChoiceList : ScriptableObject
{
    [SerializeField]
    List<string> possibleResponses;
    [SerializeField]
    List<GameObject> responseIcons;
}
