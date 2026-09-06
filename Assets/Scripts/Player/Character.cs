using UnityEngine;

public abstract class Character : MonoBehaviour , Idamageable{
    public CharacterStatsRuntime stats; 
    public CharacterStats baseStats;
    public CharacterEffectsHandler effectsHandler;
    public virtual void Awake(){
        stats = new CharacterStatsRuntime{
            maxHP = baseStats.maxHP,
            currentHP = baseStats.maxHP,
            speed = baseStats.speed,
            damage = baseStats.damage,
            defense = baseStats.defense
        };
    }
    public void TakeDamage(float damage){
        float finalDamage = damage - stats.defense;
        if(finalDamage<1) stats.currentHP-=1;
        else stats.currentHP-=finalDamage;
    }
    public void RecieveHeal(float amount){
        stats.currentHP+=amount;
        if(stats.currentHP>stats.maxHP) stats.currentHP=stats.maxHP;
    }
    public void Die(){
        effectsHandler.ApplyOnKillEffects();
    }
}
