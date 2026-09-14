using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/MelleWeapon")]
public class MelleWeaponData : WeaponData{
    public float size;
    public List<AttackData> attackSequence;
    public AttackData chargedAttackData;
    public void Reset(){
        modifiers = new List<StatModifier>{
            new StatModifier(StatType.damage,1),
            new StatModifier(StatType.size,1)
        };
    }
    protected bool Unlocked(WeaponController controller, string nodeID){
        return controller.melleProgress.IsUnlocked(nodeID);
    }
    public virtual void Fire(WeaponController controller ,byte attackIndex,FireMode fireMode, StatusEffectData statusEffect = null){
        Vector3 offset = new Vector3(0,0f,0);
        Vector3 offsetPos = controller.pivotObject.transform.position+controller.pivotObject.transform.rotation * offset;
        GameObject projectile;
        if(fireMode == FireMode.Charge){
            projectile = Instantiate(
                chargedAttackData.attackType,
                offsetPos,
                controller.pivotObject.transform.rotation
            );
        }else{
            projectile = Instantiate(
                attackSequence[attackIndex].attackType,
                offsetPos,
                controller.pivotObject.transform.rotation
            );
        }
        Camera.main.GetComponent<CameraShake>().Shake(0.1f,0.05f);
        Projectile proj = projectile.GetComponent<Projectile>();
        if(fireMode==FireMode.Charge){
            foreach(var fx in weaponEffects) fx.OnChargeRelease(controller);
            proj.damage = damage*chargeModifier[StatType.damage];
            proj.gameObject.transform.localScale = new Vector3(proj.gameObject.transform.localScale.x*size*chargeModifier[StatType.size],proj.gameObject.transform.localScale.x*size*chargeModifier[StatType.size],1);
        }else{
            proj.damage = damage;
            proj.gameObject.transform.localScale = new Vector3(proj.gameObject.transform.localScale.x*size,proj.gameObject.transform.localScale.x*size,1);
        }
        proj.weaponEffects = weaponEffects;
        proj.statusEffect = statusEffect;
        //print("melle damage -> "+proj.damage);
        proj.player = controller.player;
    }
}