using UnityEngine;
class Box : MonoBehaviour , Idamageable{
    public float hp=100;
    public void TakeDamage(float damage){
        this.hp-=damage;
        if(hp<=0) Die();
    }
    public void Die(){
        Destroy(gameObject);
        return;
    }
}