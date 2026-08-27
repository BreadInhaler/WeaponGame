using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
class WeaponController : MonoBehaviour{
    public RangedWeaponData rangedWeapon;
    public MelleWeaponData melleWeapon;
    public WeaponData selectedWeapon;
    public Dictionary<StatType,float> finalStats = new Dictionary<StatType, float>();
    public byte attackIndex = 0;
    public float chargeTimer = 0;
    public byte maxChargeTimer = 1;
    public GameObject pivotTransform;
    public InputAction fireInput;
    public InputAction switchWeaponInput;
    public void Start(){
        fireInput = InputSystem.actions.FindAction("Attack");
        fireInput.Enable();
        switchWeaponInput = InputSystem.actions.FindAction("SwitchWeapon");
        switchWeaponInput.Enable();
    }
    public void Update(){
        HandleFire(selectedWeapon);
        if(switchWeaponInput.WasPressedThisFrame()) SwitchWeapon();
    }
    public void SwitchWeapon(){
        if(selectedWeapon == melleWeapon) selectedWeapon = rangedWeapon; 
        else selectedWeapon = melleWeapon;
    }
    public void Fire(WeaponData weapon){
        return;
    }
    private void FireMelle(MelleWeaponData weapon){
        if(attackIndex < weapon.attackSequence.Count){
            GameObject pivotObject = pivotTransform.GetComponentInChildren<SpriteRenderer>().gameObject;
            GameObject projectile = Instantiate(
                weapon.attackSequence[attackIndex].attackType,
                pivotObject.transform.position,
                pivotObject.transform.rotation
            );
            Projectile proj = projectile.GetComponent<Projectile>();
            proj.damage = weapon.damage;
            proj.gameObject.transform.localScale = new Vector3(weapon.size,weapon.size,1);
            attackIndex++;
        }else{
            attackIndex = 0;
            FireMelle(weapon);
        }
    }
    private void FireRanged(RangedWeaponData weapon){
        GameObject pivotObject = pivotTransform.GetComponentInChildren<SpriteRenderer>().gameObject;
        GameObject projectile = Instantiate(
            weapon.projectilePrefabs[0],
            pivotObject.transform.position,
            pivotObject.transform.rotation
        );
        if(finalStats.Count==0){
            Projectile proj = projectile.GetComponent<Projectile>();
            proj.damage = weapon.damage*weapon.chargeModifier[StatType.damage];
            proj.speed = weapon.speed*weapon.chargeModifier[StatType.speed];
            proj.peirce = weapon.pierce*weapon.chargeModifier[StatType.peirce];
            proj.homingStrenght = weapon.homingStrenght*weapon.chargeModifier[StatType.homingStrenght];
            proj.lifeTime = weapon.lifeTime;
            proj.afterEffectSize = weapon.afterEffectSize*weapon.chargeModifier[StatType.afterEffectSize];
            proj.Init();
        }
    }
    public void HandleFire(WeaponData weapon){
        switch(weapon.fireMode){
            case FireMode.AutoFire:
                if(fireInput.WasPressedThisFrame()) {
                    if(weapon.GetType() == melleWeapon.GetType()) FireMelle(weapon as MelleWeaponData);
                    else FireRanged(weapon as RangedWeaponData);
                }
                break;
            case FireMode.Charge:
                if(fireInput.IsPressed()) chargeTimer+=Time.deltaTime;
                if(fireInput.WasReleasedThisFrame()){
                    if(weapon is MelleWeaponData chargedMelle) if(chargeTimer>=chargedMelle.chargeTime) FireMelle(melleWeapon);
                    if(weapon is RangedWeaponData rangedWeapon) if(chargeTimer>=rangedWeapon.chargeTime) FireRanged(rangedWeapon);
                    chargeTimer=0;
                }
                break;
            default:
                break;
        }
    }
    public void RecalculateStats(){
        return;
    }
}