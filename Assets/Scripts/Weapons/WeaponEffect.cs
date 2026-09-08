using UnityEngine;
public abstract class WeaponEffect : ScriptableObject{
/// <summary>Called when the attack connects with a character, before death is resolved.</summary>
    public virtual void OnHit(WeaponController source, Character target) { }
    /// <summary>Called after OnHit, only if the target died from this attack.</summary>
    public virtual void OnKill(WeaponController source, Character target) { }
    /// <summary>Called when a parry projectile successfully deflects/destroys an incoming projectile.</summary>
    public virtual void OnParry(WeaponController source) { }
    /// <summary>Called when a charged attack is released.</summary>
    public virtual void OnChargeRelease(WeaponController source) { }
}
[CreateAssetMenu(menuName = "Weapon/Effects/LifestealOnKill",fileName = "LifeStealOnKill")]
public class LifestealOnKillEffect : WeaponEffect{
    public float healPercentOfDamage = 0.1f;
 
    public override void OnKill(WeaponController source, Character target){
        var player = source.player;
        if (player == null) return;
 
        float healAmount = source.finalStats[StatType.damage] * healPercentOfDamage;
        player.RecieveHeal(healAmount);
    }
}
[CreateAssetMenu(menuName = "Weapon/Effects/LifestealOnHit",fileName = "LifeStealOnHit")]
public class LifestealOnHitEffect : WeaponEffect{
    public float healPercentOfDamage = 0.1f;
 
    public override void OnHit(WeaponController source, Character target){
        var player = source.player;
        if (player == null) return;
 
        float healAmount = source.finalStats[StatType.damage] * healPercentOfDamage;
        player.RecieveHeal(healAmount);
    }
}
[CreateAssetMenu(menuName = "Weapon/Effects/SelfDamageOnChargeAttack",fileName = "SelfDamageOnChargeAttack")]
public class SelfDamageOnChargeAttackEffect : WeaponEffect{
    public float selfHarmDamage = 10f;
 
    public override void OnChargeRelease(WeaponController source){
        var player = source.player;
        if (player == null) return;
        player.TakeDamageIgnoreDefense(selfHarmDamage);
    }
}
 