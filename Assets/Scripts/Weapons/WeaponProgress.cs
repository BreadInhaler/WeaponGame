using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponProgress {
    public WeaponData weapon;
    private WeaponController controller;
    public int currentExp;
    public List<SkillNode> unlockedNodes = new List<SkillNode>();
    public void Init(WeaponController controller, MelleWeaponData weapon){
        this.controller = controller;
        this.weapon = weapon; 
        SaveData data = SaveSystem.LoadGameData(controller.player.playerID);
        for(byte i=0;i<data.playerProfile.melleProgressData.nodes.Count;i++){
            for(byte j=0;j<weapon.skillTree.allNodes.Count;j++){
                if(data.playerProfile.melleProgressData.nodes[i] == weapon.skillTree.allNodes[j].id){
                    unlockedNodes.Add(weapon.skillTree.allNodes[j]);
                    break;
                }
            }
        }
        for(byte i=0;i<unlockedNodes.Count;i++){
            if(controller.finalStats.TryGetValue(unlockedNodes[i].statType, out float value)) controller.finalStats[unlockedNodes[i].statType] = value + unlockedNodes[i].statAmount;
            else controller.finalStats[unlockedNodes[i].statType]=unlockedNodes[i].statAmount;
        }
        controller.RecalculateStats();
    }
    public void Init(WeaponController controller, RangedWeaponData weapon){
        this.controller = controller;
        this.weapon = weapon; 
        SaveData data = SaveSystem.LoadGameData(controller.player.playerID);
        for(byte i=0;i<data.playerProfile.rangedProgressData.nodes.Count;i++){
            for(byte j=0;j<weapon.skillTree.allNodes.Count;j++){
                if(data.playerProfile.rangedProgressData.nodes[i] == weapon.skillTree.allNodes[j].id){
                    unlockedNodes.Add(weapon.skillTree.allNodes[j]);
                    break;
                }
            }
        }
        for(byte i=0;i<unlockedNodes.Count;i++){
            controller.finalStats[unlockedNodes[i].statType]+=unlockedNodes[i].statAmount;
        }
        controller.RecalculateStats();
    }
    public bool IsUnlocked(string id){
        for(byte i=0;i<unlockedNodes.Count;i++) if (unlockedNodes[i].id == id) return true;
        return false;
    }
}