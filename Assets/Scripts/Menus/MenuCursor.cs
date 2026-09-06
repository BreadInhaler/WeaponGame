using UnityEngine;
using UnityEngine.InputSystem;

public class MenuCursor : MonoBehaviour{
    public void MoveTo(Transform target, Vector3 offset){
        gameObject.SetActive(target != null);
        if(target!=null) transform.position= target.position+offset;
    }
} 