using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/RangedWeapon")]
public class RangedWeaponData : WeaponData{
    public List<GameObject> projectilePrefabs;
    public float speed;
    public float homingStrenght;
    public byte projectileCount;
    public byte pierce;
    public float lifeTime;
    public float afterEffectSize;
    public virtual void Fire(WeaponController controller, FireMode fireMode){
        GameObject projectile = Instantiate(
            projectilePrefabs[0],
            controller.pivotObject.transform.position,
            controller.pivotObject.transform.rotation
        );
        Projectile proj = projectile.GetComponent<Projectile>();
        if(fireMode == FireMode.Charge){
            proj.damage = damage*chargeModifier[StatType.damage];
            proj.speed = speed*chargeModifier[StatType.speed];
            proj.peirce = pierce*chargeModifier[StatType.peirce];
            proj.homingStrenght = homingStrenght*chargeModifier[StatType.homingStrenght];
            proj.afterEffectSize = afterEffectSize*chargeModifier[StatType.afterEffectSize];
        }else{
            proj.damage = damage;
            proj.speed = speed;
            proj.peirce = pierce;
            proj.homingStrenght = homingStrenght;
            proj.afterEffectSize = afterEffectSize;
        }
        proj.lifeTime = lifeTime;
        proj.player = controller.player;
        proj.Init();
    }
}