using UnityEngine;
using System.Collections.Generic;

public class WeaponData : ScriptableObject{
    public string id;
    public Sprite sprite;
    public float damage;
    public float lifeSteal;
    public float firerate;
    public FireMode fireMode;
    public FireMode altFireMode;
    public WeaponEffect[] weaponEffects;

    public float chargeTime;
    public SkillTree skillTree;
    public List<StatModifier> modifiers = new List<StatModifier>{
        new StatModifier(StatType.damage,1),
        new StatModifier(StatType.speed,1),
    };
    private Dictionary<StatType,float> _chargeModifierDict;
    public Dictionary<StatType, float> chargeModifier {
        get {
            if (_chargeModifierDict == null){
                _chargeModifierDict = new Dictionary<StatType, float>();
                foreach (var mod in modifiers){
                    _chargeModifierDict[mod.statType] = mod.value;
                }
            }
            return _chargeModifierDict;
        }
    }
    void OnValidate(){
        _chargeModifierDict = null; // force rebuild next time it's accessed
    }
}
[System.Serializable]
public struct StatModifier {
    public StatType statType;
    public float value;
    public StatModifier(StatType statType, float amount){
        this.statType = statType;
        this.value = amount;
    }
}

