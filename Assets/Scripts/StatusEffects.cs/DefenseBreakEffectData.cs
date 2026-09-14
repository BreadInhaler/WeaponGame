using UnityEngine;

[CreateAssetMenu(menuName = "Effect/DefenseBreak",fileName = "DefenseBreakEffect")]
public class DefenseBreakEffectData : StatusEffectData{
    public override StatusEffectRuntime CreateEffectInstance(){return new DefenseBreakEffect(this);}
}