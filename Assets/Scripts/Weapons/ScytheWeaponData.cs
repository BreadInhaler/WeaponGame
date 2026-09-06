using UnityEngine;
public class ScytheWeaponData : MelleWeaponData{
    public byte harvestSelfDamage;
    public string applyMarkedID;
    public string marked;
    protected override void OnChargeRelease(WeaponController controller){
        controller.player.TakeDamage(harvestSelfDamage);
    }
}