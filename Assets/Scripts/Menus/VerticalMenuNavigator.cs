using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class VerticalMenuNavigator : MonoBehaviour
{
    public PlayerInput playerInput; // assign THIS player's PlayerInput
    public CustomButton[] buttons;
    public string navigateAction = "Navigate";
    public string submitAction = "Submit";
    public float navCooldown = 0.2f;

    private int currentIndex = 0;
    private InputAction navigate;
    private InputAction submit;
    private float lastNavTime;

    void OnEnable(){
        if (playerInput == null){   
            Debug.LogWarning("MenuNavigator has no PlayerInput assigned!");
            return;
        }

        navigate = playerInput.actions[navigateAction];
        navigate.Enable();
        submit = playerInput.actions[submitAction];
        submit.Enable();

        submit.performed += OnSubmit;

        currentIndex = 0;
        UpdateHighlight();
    }

    void OnDisable(){
        if (submit != null) submit.performed -= OnSubmit;
    }

    void Update(){
        if (navigate == null) return;
        if (Time.unscaledTime - lastNavTime < navCooldown) return;

        Vector2 nav = navigate.ReadValue<Vector2>();
        Debug.Log("Nav input: " + nav); // ADD THIS

        if (nav.y > 0.5f) { Move(-1); lastNavTime = Time.unscaledTime; }
        else if (nav.y < -0.5f) { Move(1); lastNavTime = Time.unscaledTime; }
    }

    void Move(int dir){
        if (buttons.Length == 0) return;
        currentIndex = (currentIndex + dir + buttons.Length) % buttons.Length;
        UpdateHighlight();
    }

    void UpdateHighlight(){
        for (int i = 0; i < buttons.Length; i++) buttons[i].SetHighlighted(i == currentIndex);
    }

    void OnSubmit(InputAction.CallbackContext ctx){
        Debug.Log("Submit pressed, current index: " + currentIndex); // ADD THIS
        if (buttons.Length == 0) return;
        buttons[currentIndex].Click();
    }
}