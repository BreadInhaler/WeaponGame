using UnityEngine;
public abstract class StatusEffectData : ScriptableObject {
    public string id;
    public float effectStrength;//damage/modifier for stats
    public GameObject onKillEffect;
    [Header("Time")]
    public float duration=10f;
    public float tickInterval=1f;
    [Header("Stacks")]
    public bool stackable=false;//false by default
    public float maxStacks=1f;
    public abstract StatusEffectRuntime CreateEffectInstance();
}
[CreateAssetMenu(menuName = "Effect/Fire",fileName = "FireEffect")]
public class FireEffectData : StatusEffectData{
    public override StatusEffectRuntime CreateEffectInstance(){return new FireEffect(this);}
}
[CreateAssetMenu(menuName = "Effect/DefenseBreak",fileName = "DefenseBreakEffect")]
public class DefenseBreakEffectData : StatusEffectData{
    public override StatusEffectRuntime CreateEffectInstance(){return new DefenseBreakEffect(this);}
}
public class DeathMarkedEffectData : StatusEffectData{
    public override StatusEffectRuntime CreateEffectInstance(){return new DeathMarkedEffect(this);}
}