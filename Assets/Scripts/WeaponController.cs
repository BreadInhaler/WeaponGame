using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
class WeaponController : MonoBehaviour{
    public RangedWeaponData rangedWeapon;
    public MelleWeaponData melleWeapon;
    [HideInInspector] public WeaponData selectedWeapon;
    [HideInInspector] public Dictionary<StatType,float> finalStats = new Dictionary<StatType, float>();
    [HideInInspector] public byte attackIndex = 0;
    public float chargeTimer = 0;
    public float attackSpeedTimer= 0;
    public GameObject pivotTransform;
    private GameObject pivotObject;
    private SpriteRenderer weaponHeld;
    [HideInInspector] public InputAction fireInput;
    [HideInInspector] public InputAction altFireInput;
    [HideInInspector] public InputAction switchWeaponInput;
    [HideInInspector] public Player player;
    
    public void Start(){
        player=GetComponent<Player>();
        selectedWeapon = rangedWeapon;
        pivotObject = pivotTransform.GetComponentInChildren<SpriteRenderer>().gameObject;
        weaponHeld = player.GetComponentsInChildren<SpriteRenderer>()[2];
        SwitchWeapon();
        /*fireInput = InputSystem.actions.FindAction("Fire");
        fireInput.Enable();
        altFireInput = InputSystem.actions.FindAction("AltFire");
        altFireInput.Enable();
        switchWeaponInput = InputSystem.actions.FindAction("SwitchWeapon");
        switchWeaponInput.Enable();*/
        fireInput = player.playerInput.actions.FindAction("Fire");
        fireInput.Enable();
        altFireInput = player.playerInput.actions.FindAction("AltFire");
        altFireInput.Enable();
        switchWeaponInput = player.playerInput.actions.FindAction("SwitchWeapon");
        switchWeaponInput.Enable();
    }
    public void Update(){
        if(GameManager.Instance.IsPaused()) return;
        HandleFire(selectedWeapon.fireMode,fireInput);
        if(selectedWeapon.altFireMode!=FireMode.None) HandleFire(selectedWeapon.altFireMode,altFireInput);
        if(switchWeaponInput.WasPressedThisFrame()) SwitchWeapon();
    }
    public void SwitchWeapon(){
        if(selectedWeapon == melleWeapon) selectedWeapon = rangedWeapon; 
        else selectedWeapon = melleWeapon;
        weaponHeld.sprite=selectedWeapon.sprite;
    }
    public void Fire(WeaponData weapon){
        return;
    }
    private void FireMelle(MelleWeaponData weapon,FireMode fireMode){
        if(attackIndex >= weapon.attackSequence.Count) attackIndex = 0;
        Vector3 offset = new Vector3(0,0f,0);
        Vector3 offsetPos = pivotObject.transform.position+pivotObject.transform.rotation * offset;
        GameObject projectile = Instantiate(
            weapon.attackSequence[attackIndex].attackType,
            offsetPos,
            pivotObject.transform.rotation
        );
        Camera.main.GetComponent<CameraShake>().Shake(0.1f,0.05f);
        Projectile proj = projectile.GetComponent<Projectile>();
        if(fireMode==FireMode.Charge){
            proj.damage = weapon.damage*weapon.chargeModifier[StatType.damage];
            proj.gameObject.transform.localScale = new Vector3(proj.gameObject.transform.localScale.x*weapon.size*weapon.chargeModifier[StatType.size],proj.gameObject.transform.localScale.x*weapon.size*weapon.chargeModifier[StatType.size],1);
        }else{
            proj.damage = weapon.damage;
            proj.gameObject.transform.localScale = new Vector3(proj.gameObject.transform.localScale.x*weapon.size,proj.gameObject.transform.localScale.x*weapon.size,1);
        }
        //print("melle damage -> "+proj.damage);
        proj.player = player;
        attackIndex++;
    }
    private void FireRanged(RangedWeaponData weapon,FireMode fireMode){
        GameObject projectile = Instantiate(
            weapon.projectilePrefabs[0],
            pivotObject.transform.position,
            pivotObject.transform.rotation
        );
        Projectile proj = projectile.GetComponent<Projectile>();
        if(fireMode == FireMode.Charge){
            if(finalStats.Count==0){
                proj.damage = weapon.damage*weapon.chargeModifier[StatType.damage];
                proj.speed = weapon.speed*weapon.chargeModifier[StatType.speed];
                proj.peirce = weapon.pierce*weapon.chargeModifier[StatType.peirce];
                proj.homingStrenght = weapon.homingStrenght*weapon.chargeModifier[StatType.homingStrenght];
                proj.afterEffectSize = weapon.afterEffectSize*weapon.chargeModifier[StatType.afterEffectSize];
            }
        }else{
            if(finalStats.Count==0){
                proj.damage = weapon.damage;
                proj.speed = weapon.speed;
                proj.peirce = weapon.pierce;
                proj.homingStrenght = weapon.homingStrenght;
                proj.afterEffectSize = weapon.afterEffectSize;
            }
        }
        proj.lifeTime = weapon.lifeTime;
        proj.player = player;
        proj.Init();
    }
    public void HandleFire(FireMode fireMode , InputAction input){
        switch(fireMode){
            case FireMode.AutoFire:
                if(input.IsPressed()) {
                    attackSpeedTimer+=Time.deltaTime;
                    if(attackSpeedTimer>=selectedWeapon.firerate){
                        attackSpeedTimer=0;
                        if(selectedWeapon.GetType() == melleWeapon.GetType()) FireMelle(selectedWeapon as MelleWeaponData,fireMode);
                        else FireRanged(selectedWeapon as RangedWeaponData,fireMode);
                    }
                }
                if(input.WasReleasedThisFrame()) attackSpeedTimer=selectedWeapon.firerate;
                break;
            case FireMode.Charge:
                if(input.IsPressed()) chargeTimer+=Time.deltaTime;
                if(input.WasReleasedThisFrame()){
                    if(selectedWeapon is MelleWeaponData chargedMelle) if(chargeTimer>=chargedMelle.chargeTime) FireMelle(melleWeapon,fireMode);
                    if(selectedWeapon is RangedWeaponData rangedWeapon) if(chargeTimer>=rangedWeapon.chargeTime) FireRanged(rangedWeapon,fireMode);
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