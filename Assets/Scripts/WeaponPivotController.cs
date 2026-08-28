using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

class WeaponPivotController : MonoBehaviour{
    Camera cam;
    PlayerInput playerInput;
    InputAction aimAction;
    public void Start(){
        cam = Camera.main;
        playerInput=GetComponentInParent<PlayerInput>();
        aimAction = playerInput.actions.FindAction("Look");
    }
    public void Update(){
        float angle;
        if (playerInput.currentControlScheme == "Gamepad"){
            Vector2 stickDir = aimAction.ReadValue<Vector2>();

            // avoid snapping to 0 when stick is neutral - keep last angle
            if (stickDir.sqrMagnitude < 0.01f)
                return;

            angle = Mathf.Atan2(stickDir.y, stickDir.x) * Mathf.Rad2Deg;
        }
        else{
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -cam.transform.position.z));
            mouseWorldPos.z = 0f;

            Vector2 direction = (Vector2)mouseWorldPos - (Vector2)transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}