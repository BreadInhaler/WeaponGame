using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SkillNode")]
public class SkillNode : ScriptableObject{
    public string id;
    public StatType statType;
    public float statAmount;
    public int upgradeCost;
    public StatusEffectData statusEffect;
    public List<SkillNode> prevNodes = new List<SkillNode>();
    public List<SkillNode> nextNodes = new List<SkillNode>();
}