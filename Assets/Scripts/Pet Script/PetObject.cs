using UnityEngine;

[CreateAssetMenu(fileName = "NewPet", menuName = "ScriptableObjects/Create Pet", order = 1)]
[System.Serializable]
public class PetObject : ScriptableObject
{
    public string petName;
    public float happiness, maxHapp, fullness, maxFull, level;
}
