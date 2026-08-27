using UnityEngine;

class WeaponData : ScriptableObject{
    public string id;
    public Sprite sprite;
    public float damage;
    public float firerate;
    public FireMode fireMode;
    public FireMode altFireMode;
}
