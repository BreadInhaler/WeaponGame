using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine;


class Player : Character{
    public byte playerIndex;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public WeaponController weaponController;
    public override void Awake(){
        base.Awake();
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
    }
    public void LoadPlayer(string id){
        return;
    }
    public void Update(){
        
    }
}
