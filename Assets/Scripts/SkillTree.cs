using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SkillTree")]
class SkillTree : ScriptableObject{
    SkillNode rootNode;
    List<SkillNode> allNodes = new List<SkillNode>();
}