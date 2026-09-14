using UnityEngine;

[CreateAssetMenu(menuName = "Effect/DeathMarked",fileName = "DeathMarked")]
public class DeathMarkedEffectData : StatusEffectData{
    public override StatusEffectRuntime CreateEffectInstance(){return new DeathMarkedEffect(this);}
}