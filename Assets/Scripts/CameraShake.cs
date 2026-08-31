using UnityEngine;

public class CameraShake : MonoBehaviour{
    private Vector3 shakeOffset;
    private float shakeDuration = 0f;
    private float originalDuration = 0f;
    private float shakeMagnitude = 0f;
    private float dampingSpeed = 1f;
    /// <summary>
    /// Triggers a camera shake. If a stronger shake is already playing, this call is ignored.
    /// </summary>
    public void Shake(float duration, float magnitude, float damping = 1f){
        print("shake reached - duration - "+duration+" - magnitude - "+magnitude+" - damping - "+damping);
        // Ignore new shake if current one is already stronger
        if (shakeDuration > 0f && shakeMagnitude > magnitude) return;

        shakeDuration = duration;
        originalDuration = duration;
        shakeMagnitude = magnitude;
        dampingSpeed = damping;
    }

    void Update(){
        if(GameManager.Instance.IsPaused()) return;
        if (shakeDuration > 0){
            float decayRatio = shakeDuration / originalDuration;
            shakeOffset = Random.insideUnitCircle * shakeMagnitude * decayRatio;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }else{
            shakeDuration = 0f;
            shakeMagnitude = 0f;
            shakeOffset = Vector3.zero;
        }
    }

    public Vector3 GetShakeOffset(){return shakeOffset;}
}