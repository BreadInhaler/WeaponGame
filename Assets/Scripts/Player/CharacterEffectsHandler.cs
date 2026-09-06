using System.Collections.Generic;
using UnityEngine;
public class CharacterEffectsHandler{
    public Character character;
    List<StatusEffectRuntime> statusEffects = new List<StatusEffectRuntime>();
    public void Init(Character character){
        this.character = character;
    }
    public void TickStatusEffects(float deltaTime){
        List<StatusEffectRuntime> toTick = new List<StatusEffectRuntime>(statusEffects);
        foreach(StatusEffectRuntime effect in toTick){
            effect.elapsedTime+=deltaTime;
            effect.tickTimer+=deltaTime;

            if(effect.tickTimer >= effect.effectData.tickInterval){
                effect.tickTimer-=effect.effectData.tickInterval;
                effect.OnTick(this);
            }
            if(effect.effectData.duration <= effect.elapsedTime){
                effect.OnRemove(this);
            }
        }
    }
    public void ApplyStatusEffect(StatusEffectRuntime effect){
        statusEffects.Add(effect);
        //print(gameObject.name+" recieved "+effect.effectData.id);
    }
    public void RemoveStatusEffect(StatusEffectRuntime effect){
        statusEffects.Remove(effect);
    }
    public void RemoveAllStatusEffects(){
        List<StatusEffectRuntime> toRemove = new List<StatusEffectRuntime>(statusEffects);
        foreach(StatusEffectRuntime effect in toRemove){
            effect.OnRemove(this);
        }
        statusEffects.Clear();
    }
    public List<StatusEffectRuntime> GetStatusEffects(){
        return this.statusEffects;
    }
    public void ApplyOnKillEffects(){
        for(byte i=0;i<statusEffects.Count;i++){
            statusEffects[i].OnKill(this);
        }
    }
}