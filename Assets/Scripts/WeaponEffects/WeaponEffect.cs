using UnityEngine;
public abstract class WeaponEffect : ScriptableObject{
    // <summary>Called when the attack connects with a character, before death is resolved.</summary>
    [Header("0-OnHit|1-OnKill!2-OnParry|3-OnChargeRelease")]
    public bool[] does = new bool[4];
    public void Reset(){
        for(byte i=0;i<does.Length;i++) does[i]=false;
    }
    public virtual void OnHit(WeaponController source, Character target) { }
    // <summary>Called after OnHit, only if the target died from this attack.</summary>
    public virtual void OnKill(WeaponController source, Character target) { }
    // <summary>Called when a parry projectile successfully deflects/destroys an incoming projectile.</summary>
    public virtual void OnParry(WeaponController source) { }
    // <summary>Called when a charged attack is released.</summary>
    public virtual void OnChargeRelease(WeaponController source) { }
}
