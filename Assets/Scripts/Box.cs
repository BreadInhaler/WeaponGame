using UnityEngine;
class Box : MonoBehaviour , Idamageable{
    public float hp=100;
    public bool TakeDamage(float damage){
        this.hp-=damage;
        if(hp<=0){
            Die();
            return true;
        }
        return false;
    }
    public void Die(){
        Destroy(gameObject);
        return;
    }
}