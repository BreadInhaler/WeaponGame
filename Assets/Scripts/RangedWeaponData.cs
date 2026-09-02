using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/RangedWeapon")]
public class RangedWeaponData : WeaponData{
    public List<GameObject> projectilePrefabs;
    public float speed;
    public float homingStrenght;
    public byte projectileCount;
    public byte pierce;
    public float lifeTime;
    public float afterEffectSize;
}