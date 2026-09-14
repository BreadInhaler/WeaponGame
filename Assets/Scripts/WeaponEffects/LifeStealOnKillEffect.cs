using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/Effects/LifestealOnKill",fileName = "LifeStealOnKill")]
public class LifestealOnKillEffect : WeaponEffect{
    public float healPercentOfDamage = 0.1f;
    public void Reset(){
        does[1]=true;
    }
    public override void OnKill(WeaponController source, Character target){
        var player = source.player;
        if (player == null) return;
 
        float healAmount = source.finalStats[StatType.damage] * healPercentOfDamage;
        player.RecieveHeal(healAmount);
    }
}