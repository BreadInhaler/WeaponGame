using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/ChargedMelleWeapon")]
class ChargedMelleWeaponData : MelleWeaponData{
    float chargeTime;
    Dictionary<StatType,float> chargeModifier;
}