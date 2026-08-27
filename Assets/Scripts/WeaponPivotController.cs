using UnityEngine;
using UnityEngine.InputSystem;

class WeaponPivotController : MonoBehaviour{
    Camera cam;
    public void Start(){
        cam = Camera.main;
    }
    public void Update(){
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -cam.transform.position.z));
        mouseWorldPos.z = 0f;

        Vector2 direction = (Vector2)mouseWorldPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}