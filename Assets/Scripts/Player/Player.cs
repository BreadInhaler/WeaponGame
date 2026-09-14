using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine;


public class Player : Character{
    public GameMenuManager menuManager;
    public Animator animator;
    public byte playerIndex;
    public byte playerID;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public WeaponController weaponController;
    private InputAction testInput;
    [HideInInspector]public InputAction menuInput;
    public GameMenuManager gameMenu;
    public override void Awake(){
        base.Awake();
        animator = GetComponentInChildren<Animator>();
        playerInput=GetComponent<PlayerInput>();
        weaponController = GetComponent<WeaponController>();
        playerInput.user.UnpairDevices();
        if (playerIndex == 0){
            InputUser.PerformPairingWithDevice(Keyboard.current, playerInput.user);
            InputUser.PerformPairingWithDevice(Mouse.current, playerInput.user);
            playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
        }
        else{
            var gamepad = Gamepad.all.Count > 0 ? Gamepad.all[playerIndex-1] : null;
            if (gamepad != null){
                InputUser.PerformPairingWithDevice(gamepad, playerInput.user);
                playerInput.SwitchCurrentControlScheme("Gamepad", gamepad);
            }
        }
        //testInput = playerInput.actions.FindAction("Test");
        //testInput.Enable();
        menuInput = playerInput.actions.FindAction("Menu");
        menuInput.Enable();
    }
    public override bool TakeDamage(float damage){
        float finalDamage = damage - stats.defense;
        if(finalDamage<1) stats.currentHP-=1;
        else stats.currentHP-=finalDamage;
        menuManager.HUDPanel.GetComponent<HUDHandler>().UpdateHealthBar(playerIndex,stats.currentHP,stats.maxHP);
        return stats.currentHP <=0;
    }
    public override bool TakeDamageIgnoreDefense(float damage){
        if(damage<1) stats.currentHP-=1;
        else stats.currentHP-=damage;
        menuManager.HUDPanel.GetComponent<HUDHandler>().UpdateHealthBar(playerIndex,stats.currentHP,stats.maxHP);
        return stats.currentHP <=0;
    }
    public override void RecieveHeal(float amount){
        base.RecieveHeal(amount);
        menuManager.HUDPanel.GetComponent<HUDHandler>().UpdateHealthBar(playerIndex,stats.currentHP,stats.maxHP);
    }
    
    public void LoadPlayer(string id){
        return;
    }
    public void Update(){
        //if(testInput.WasPressedThisFrame()) Camera.main.GetComponent<CameraShake>().Shake(0.1f,0.05f);
        if(menuInput.WasPressedThisFrame()) gameMenu.ToggleMenu(this);
    }
}
