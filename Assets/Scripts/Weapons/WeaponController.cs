using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponController : MonoBehaviour{
    private RangedWeaponData rangedWeapon;
    private MelleWeaponData melleWeapon;
    public WeaponData selectedWeapon;
    public WeaponProgress melleProgress = new WeaponProgress();
    public WeaponProgress rangedProgress = new WeaponProgress();
    //public StatusEffectData currentEffect;
    public Dictionary<StatType,float> finalStats = new Dictionary<StatType, float>();
    [HideInInspector] public byte attackIndex = 0;
    [HideInInspector] public FireMode lastAttack;
    [HideInInspector] public bool altFireUnlocked=false;
    public float chargeTimer = 0;
    public float attackSpeedTimer= 0;
    public GameObject pivotTransform;
    [HideInInspector]public GameObject pivotObject;
    private SpriteRenderer weaponHeld;
    [HideInInspector] public InputAction fireInput;
    [HideInInspector] public InputAction altFireInput;
    [HideInInspector] public InputAction switchWeaponInput;
    [HideInInspector] public Player player;
    public SpriteRenderer chargeFill;
    MaterialPropertyBlock mpb;
    static readonly int FillAmountID = Shader.PropertyToID("_FillAmount");
    
    public void Start(){
        player=GetComponent<Player>();
        pivotObject = pivotTransform.GetComponentInChildren<SpriteRenderer>().gameObject;
        weaponHeld = player.GetComponentsInChildren<SpriteRenderer>()[1];
        SaveData data = SaveSystem.LoadGameData(player.playerID);
        melleWeapon = LookUpResources.GetMelleWeaponByID(data.playerProfile.melleWeapon);
        rangedWeapon = LookUpResources.GetRangedWeaponByID(data.playerProfile.rangedWeapon);
        selectedWeapon = rangedWeapon;
        SwitchWeapon();
        //melleProgress.Init(this,melleWeapon);
        InitInputs();
        mpb = new MaterialPropertyBlock();
        chargeFill.GetComponentsInParent<SpriteRenderer>(true)[1].gameObject.SetActive(false);
    }
    public void Update(){
        if(GameManager.Instance.IsPaused()) return;
        HandleFire(selectedWeapon.fireMode,fireInput);
        //if(altFireUnlocked) 
        if(selectedWeapon.altFireMode!=FireMode.None) HandleFire(selectedWeapon.altFireMode,altFireInput);
        if(switchWeaponInput.WasPressedThisFrame()) SwitchWeapon();
        attackSpeedTimer+=Time.deltaTime;
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
        lastAttack = fireMode;
        if(attackIndex >= weapon.attackSequence.Count) attackIndex = 0;
        //animator.SetBool("isSwinging",true);
        weapon.Fire(this,attackIndex,fireMode);
        attackIndex++;

    }
    private void FireRanged(RangedWeaponData weapon,FireMode fireMode){
        weapon.Fire(this,fireMode);
    }
    public void HandleFire(FireMode fireMode,InputAction input){
        print(Time.timeScale+" x fast");
        switch(fireMode){
            case FireMode.AutoFire:
                if(input.IsPressed()==false) return;
                if(attackSpeedTimer>=selectedWeapon.firerate){
                    attackSpeedTimer=0;
                    if(selectedWeapon.GetType() == melleWeapon.GetType()) FireMelle(selectedWeapon as MelleWeaponData,fireMode);
                    else FireRanged(selectedWeapon as RangedWeaponData,fireMode);
                }
                break;
            case FireMode.Charge:
                if(input.IsPressed()){
                    chargeTimer+=Time.deltaTime;
                    chargeFill.GetComponentsInParent<SpriteRenderer>(true)[1].gameObject.SetActive(true);
                    SetFill(chargeTimer/selectedWeapon.chargeTime);
                } 
                if(input.WasReleasedThisFrame()){
                    if(selectedWeapon is MelleWeaponData chargedMelle) if(chargeTimer>=chargedMelle.chargeTime) FireMelle(melleWeapon,fireMode);
                    if(selectedWeapon is RangedWeaponData rangedWeapon) if(chargeTimer>=rangedWeapon.chargeTime) FireRanged(rangedWeapon,fireMode);
                    chargeTimer=0;
                    chargeFill.GetComponentsInParent<SpriteRenderer>(true)[1].gameObject.SetActive(false);
                    SetFill(0);
                }
                break;
            default:
                break;
        }
    }
    public void RecalculateStats(){
        //if(!finalStats.ContainsKey(StatType.altFireUnlocked)) return;
        if(finalStats[StatType.altFireUnlocked]>0) altFireUnlocked=true;
        else altFireUnlocked=false;
    }
    private void InitInputs(){
        fireInput = player.playerInput.actions.FindAction("Fire");
        fireInput.Enable();
        altFireInput = player.playerInput.actions.FindAction("AltFire");
        altFireInput.Enable();
        switchWeaponInput = player.playerInput.actions.FindAction("SwitchWeapon");
        switchWeaponInput.Enable();
    }
    public void SetFill(float amount){
        chargeFill.GetPropertyBlock(mpb);
        mpb.SetFloat(FillAmountID, Mathf.Clamp01(amount));
        chargeFill.SetPropertyBlock(mpb);
    }
}