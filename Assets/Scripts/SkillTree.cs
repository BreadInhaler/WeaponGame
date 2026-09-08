using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SkillTree")]
public class SkillTree : ScriptableObject{
    public SkillNode rootNode;
    public List<SkillNode> allNodes = new List<SkillNode>();
}