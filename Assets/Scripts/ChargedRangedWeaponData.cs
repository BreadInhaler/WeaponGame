using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/ChargedRangedWeapon")]
class ChargedRangedWeaponData : RangedWeaponData{
    float chargeTime;
    Dictionary<StatType,float> chargeModifier;
}