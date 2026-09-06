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

    protected virtual void OnKill(WeaponController controller,Enemy enemy){}
    protected virtual void OnChargeRelease(WeaponController controller){}
    protected virtual void OnHit(WeaponController controller, Enemy enemy, Projectile projectile){}
    protected virtual void OnParry(WeaponController controller, Enemy enemy){}

    public float chargeTime;
    public List<StatModifier> modifiers = new List<StatModifier>{
        new StatModifier(StatType.damage,1),
        new StatModifier(StatType.speed,1),
        new StatModifier(StatType.firerate,1),
        new StatModifier(StatType.peirce,1),
        new StatModifier(StatType.homingStrenght,1),
        new StatModifier(StatType.afterEffectSize,1),
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

