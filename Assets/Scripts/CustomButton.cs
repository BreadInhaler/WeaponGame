using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CustomButton : MonoBehaviour
{
    public SpriteRenderer sr;
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.yellow;
    public UnityEvent onClick;
    private Camera target;

    private bool isHighlighted;
    void Awake(){
        sr = GetComponentInChildren<SpriteRenderer>();
        SetHighlighted(false);
        foreach(GameObject root in gameObject.scene.GetRootGameObjects()) {
            Camera cam = root.GetComponent<Camera>();
            if(cam!=null) if(cam != Camera.main) target = cam;
        }
        if(target==null) target=Camera.main;
    }
    void Update(){
        if(Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame){
            Debug.Log("Mouse button pressed"); // 1
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector2 mouseWorldPos = target.ScreenToWorldPoint(mouseScreenPos);
            Debug.Log("World pos: " + mouseWorldPos); // 2
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
            Debug.Log("Hit: " + (hit != null ? hit.gameObject.name : "NOTHING")); // 3
            if (hit != null && hit.gameObject == this.gameObject) Click();
        }
    }

    public void SetHighlighted(bool state){
        isHighlighted = state;
        print(sr.name + "exists");
        if (sr != null) sr.color = state ? highlightedColor : normalColor;
    }

    public void Click(){
        Debug.Log("Button clicked: " + gameObject.name);
        onClick.Invoke();
    }

    // Optional: mouse support via collider click
    void OnMouseDown(){
        Click();
    }
}