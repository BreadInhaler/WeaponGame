using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Effects/LifestealOnHit",fileName = "LifeStealOnHit")]
public class LifestealOnHitEffect : WeaponEffect{
    public float healPercentOfDamage = 0.1f;
    public void Reset(){
        does[0]=true;
    }
    public override void OnHit(WeaponController source, Character target){
        var player = source.player;
        if (player == null) return;
 
        //float healAmount = source.finalStats[StatType.damage] * healPercentOfDamage;
        float healAmount = source.selectedWeapon.damage * healPercentOfDamage;
        player.RecieveHeal(healAmount);
    }
}