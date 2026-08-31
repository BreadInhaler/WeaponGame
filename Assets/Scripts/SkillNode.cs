using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SkillNode")]
public class SkillNode : ScriptableObject{
    string id;
    StatType statType;
    float statAmount;
    int upgradeCost;
    byte maxUpgrades;
    List<SkillNode> prevNodes = new List<SkillNode>();
    List<SkillNode> nextNodes = new List<SkillNode>();
}