using UnityEngine;

public abstract class Character : MonoBehaviour , Idamageable{
    public CharacterStatsRuntime stats; 
    public CharacterStats baseStats;
    public CharacterEffectsHandler effectsHandler = new CharacterEffectsHandler();
    public virtual void Awake(){
        stats = new CharacterStatsRuntime{
            maxHP = baseStats.maxHP,
            currentHP = baseStats.maxHP,
            speed = baseStats.speed,
            damage = baseStats.damage,
            defense = baseStats.defense
        };
        effectsHandler.Init(this);
    }
    public virtual bool TakeDamage(float damage){
        float finalDamage = damage - stats.defense;
        if(finalDamage<1) stats.currentHP-=1;
        else stats.currentHP-=finalDamage;
        return stats.currentHP <=0;
    }
    public virtual bool TakeDamageIgnoreDefense(float damage){
        if(damage<1) stats.currentHP-=1;
        else stats.currentHP-=damage;
        return stats.currentHP <=0;
    }
    public virtual void RecieveHeal(float amount){
        stats.currentHP+=amount;
        if(stats.currentHP>stats.maxHP) stats.currentHP=stats.maxHP;
    }
    public virtual void Die(){
        effectsHandler.ApplyOnKillEffects();
    }
    public CharacterStatsRuntime RefreshStats(){
        CharacterStatsRuntime current=stats.CloneStats();
        foreach(StatusEffectRuntime effect in effectsHandler.GetStatusEffects()) effect.ModifyStats(current);
        //print(gameObject.name+" 's defense: "+current.defense);
        return current;
    }
}
