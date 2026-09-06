using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiPlayerMenuNavigator : MonoBehaviour{
    public List<PlayerMenuSlot> playerSlots = new List<PlayerMenuSlot>();
    public CustomButton defaultStartingButton; // e.g. Profile1Box
    public string navigateAction = "Navigate";
    public string submitAction = "Submit";
    public float navCooldown = 0.2f;
    public Vector3 cursorOffset = new Vector3(-0.7f, 0, 0);

    public void SetupPlayers(List<PlayerInput> activePlayers){
        playerSlots = new List<PlayerMenuSlot>();
        MenuCursor[] cursors = GetComponentsInChildren<MenuCursor>();
        for(byte i=0;i<activePlayers.Count;i++){
            PlayerMenuSlot slot = new PlayerMenuSlot{
                playerInput = activePlayers[i],
                startingButton = defaultStartingButton,
                cursor = cursors[i]
            }; 
            slot.navigate = activePlayers[i].actions[navigateAction];
            slot.submit = activePlayers[i].actions[submitAction];
            slot.currentButton = slot.startingButton;

            // Subscribe ONCE per slot, right here — not in Update()
            PlayerMenuSlot capturedSlot = slot;
            slot.submit.performed += ctx => OnSubmit(capturedSlot);

            playerSlots.Add(slot);
            UpdateCursor(slot);
        }
    }

    void Update(){
        foreach (var slot in playerSlots){
            if (slot.navigate == null || slot.currentButton == null) continue;
            if (Time.unscaledTime - slot.lastNavTime < navCooldown) continue;

            Vector2 nav = slot.navigate.ReadValue<Vector2>();
            CustomButton next = null;

            if (nav.y > 0.5f) next = slot.currentButton.up;
            else if (nav.y < -0.5f) next = slot.currentButton.down;
            else if (nav.x > 0.5f) next = slot.currentButton.right;
            else if (nav.x < -0.5f) next = slot.currentButton.left;

            if (next != null){
                slot.currentButton = next;
                UpdateCursor(slot);
                slot.lastNavTime = Time.unscaledTime;
            }
        }
    }

    void UpdateCursor(PlayerMenuSlot slot){
        Debug.Log("UpdateCursor called, cursor null: " + (slot.cursor == null) + ", currentButton: " + (slot.currentButton != null ? slot.currentButton.name : "NULL"));
        if (slot.cursor != null) slot.cursor.MoveTo(slot.currentButton != null ? slot.currentButton.transform : null, cursorOffset);
    }
    void OnSubmit(PlayerMenuSlot slot){
        Debug.Log("Submit from player " + slot.playerInput.playerIndex + ", button: " + slot.currentButton.name);
        slot.currentButton?.Click();
    }
}
[System.Serializable]
public class PlayerMenuSlot{
    public PlayerInput playerInput;
    public CustomButton startingButton;
    public MenuCursor cursor;

    [HideInInspector] public CustomButton currentButton;
    [HideInInspector] public InputAction navigate;
    [HideInInspector] public InputAction submit;
    [HideInInspector] public float lastNavTime;
}