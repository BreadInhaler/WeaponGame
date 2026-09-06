using UnityEngine;
public abstract class StatusEffectRuntime{
    public StatusEffectData effectData;
    public float tickTimer;
    public float elapsedTime;
    public int stacks=1;

    public StatusEffectRuntime(StatusEffectData statusEffectSO){this.effectData = statusEffectSO;}

    public virtual void OnApply(CharacterEffectsHandler character, StatusEffectRuntime newEffect){
        foreach(StatusEffectRuntime statusEffect in character.GetStatusEffects()){
            if(statusEffect.effectData.id == newEffect.effectData.id){
                if(statusEffect.effectData.stackable)
                    statusEffect.stacks++;
                statusEffect.elapsedTime = 0f;
                statusEffect.tickTimer = 0f;
                return;
            }
        }
        character.ApplyStatusEffect(newEffect);
        newEffect.elapsedTime = 0f;
        newEffect.tickTimer = 0f;
    }
    public virtual void OnKill(CharacterEffectsHandler character){return;}
    public virtual void OnTick(CharacterEffectsHandler character){return;}//do Nothing by default in case of not being a damaging effect
    public virtual void OnRemove(CharacterEffectsHandler character){character.RemoveStatusEffect(this);}
    public virtual void ModifyStats(CharacterStatsRuntime stats){return;}//do Nothing by default in case of not being a stat modifying effect
}

public class FireEffect : StatusEffectRuntime{
    public FireEffect(StatusEffectData statusEffectSO) : base(statusEffectSO){}

    public override void OnTick(CharacterEffectsHandler character){
        if(effectData.stackable) character.character.TakeDamage(effectData.effectStrength*stacks);
        else character.character.TakeDamage(effectData.effectStrength);
    }
}
public class DefenseBreakEffect : StatusEffectRuntime{
    public DefenseBreakEffect(StatusEffectData statusEffectSO) : base(statusEffectSO){}

    public override void ModifyStats(CharacterStatsRuntime stats){
        if(effectData.stackable) stats.defense=stats.defense-(effectData.effectStrength*stacks);
        else stats.defense=stats.defense-effectData.effectStrength;
    }
}//add more
public class DeathMarkedEffect : StatusEffectRuntime{
    public DeathMarkedEffect(StatusEffectData statusEffectSO) : base(statusEffectSO){}
    public override void OnKill(CharacterEffectsHandler character){
        Object.Instantiate(effectData.onKillEffect, character.character.transform.position , new Quaternion(0,0,0,0));
    }
}