using UnityEngine;
public class Enemy : Character{
    void Update(){
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        print("enemy colided");
        Player player = collision.gameObject.GetComponent<Player>();
        if(player!=null) player.TakeDamage(stats.damage);
    }
    public override bool TakeDamage(float damage){
        float finalDamage = damage - stats.defense;
        if(finalDamage<1) stats.currentHP-=1;
        else stats.currentHP-=finalDamage;

        bool isDead = stats.currentHP <= 0;
        if(isDead) Die(); // may call Destroy(gameObject) internally — safe, deferred to end of frame
        return isDead;
    }
    public override void Die(){
        base.Die();
        Destroy(gameObject);
    }

}