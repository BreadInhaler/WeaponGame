using UnityEngine;
[CreateAssetMenu(menuName = "Effect/Fire",fileName = "FireEffect")]
public class FireEffectData : StatusEffectData{
    public override StatusEffectRuntime CreateEffectInstance(){return new FireEffect(this);}
}