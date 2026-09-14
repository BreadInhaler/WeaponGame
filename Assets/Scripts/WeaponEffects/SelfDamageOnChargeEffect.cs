using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/Effects/SelfDamageOnCharge",fileName = "SelfDamageOnChargeAttack")]
public class SelfDamageOnChargeEffect : WeaponEffect{
    public float selfHarmDamage = 10f;
    public void Reset(){
        does[3]=true;
    }
 
    public override void OnChargeRelease(WeaponController source){
        var player = source.player;
        if (player == null) return;
        player.TakeDamageIgnoreDefense(selfHarmDamage);
    }
}
 