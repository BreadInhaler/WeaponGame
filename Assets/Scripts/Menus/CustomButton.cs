using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CustomButton : MonoBehaviour{
    public SpriteRenderer sr;
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.yellow;
    public UnityEvent onClick;
    private Camera target;
    private bool isHighlighted;
    private static int lastConsumedFrame = -1; // shared across ALL buttons
    [Header("Navigation")]
    public CustomButton up;
    public CustomButton down;
    public CustomButton left;
    public CustomButton right;
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
        if (target == null) return;
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame) return;
        if (Time.frameCount == lastConsumedFrame) return; // someone else already handled this frame's click

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = target.ScreenToWorldPoint(mouseScreenPos);
        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

        if (hit != null && hit.gameObject == this.gameObject){
            lastConsumedFrame = Time.frameCount; // mark this frame's click as handled
            Click();
        }
    }
    public void SetHighlighted(bool state){
        isHighlighted = state;
        print(sr.name + "exists");
        if (sr != null) sr.color = state ? highlightedColor : normalColor;
    }

    public void Click(){
        //Debug.Log("Button clicked: " + gameObject.name);
        onClick.Invoke();
    }
}