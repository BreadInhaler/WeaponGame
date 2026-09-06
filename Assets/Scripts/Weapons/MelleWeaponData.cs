using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/MelleWeapon")]
public class MelleWeaponData : WeaponData{
    public float size;
    public List<AttackData> attackSequence;
}