using UnityEngine;

[CreateAssetMenu(fileName = "RewardBadge", menuName = "ScriptableObjects/Create Reward Badge", order = 3)]
public class RewardSO : ScriptableObject
{
    public string badgeName, badgeDescription;
    public Sprite badgeImage;

}
