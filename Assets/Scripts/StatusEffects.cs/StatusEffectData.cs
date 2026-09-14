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
    public byte maxStacks=1;
    public abstract StatusEffectRuntime CreateEffectInstance();
     // Entry point every caller should use. Checks first, only allocates a
    // runtime instance if this effect isn't already active on the target.
    public void Apply(CharacterEffectsHandler character){
        foreach (StatusEffectRuntime existing in character.GetStatusEffects()){
            if (existing.effectData.id == id){
                if (stackable) existing.stacks = (byte)Mathf.Min(existing.stacks + 1, maxStacks);
                existing.elapsedTime = 0f;
                existing.tickTimer = 0f;
                return; // no instance ever created
            }
        }

        StatusEffectRuntime newEffect = CreateEffectInstance(); // only reached when genuinely new
        newEffect.elapsedTime = 0f;
        newEffect.tickTimer = 0f;
        character.ApplyStatusEffect(newEffect);
        newEffect.OnApply(character); // one-time setup hook (VFX, etc.), not the stack logic anymore
    }
}