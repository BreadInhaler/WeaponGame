using System.Collections.Generic;
using UnityEngine;
public class WeaponProgress {
    public WeaponData weapon;
    public int currentExp;
    List<SkillNode> unlockedNodes = new List<SkillNode>();
    public bool IsUnlocked(string id){
        for(byte i=0;i<unlockedNodes.Count;i++) if (unlockedNodes[i].id == id) return true;
        return false;
    }
}