using UnityEditor.Tilemaps;
using UnityEngine; 

public class Projectile : MonoBehaviour{
    public Player player;
    public GameObject afterEffect;
    public float lifeTime;
    public float peirce;
    public float speed;
    public float damage;
    public float homingStrenght;
    public float afterEffectSize;
    public StatusEffectData statusEffect;
    public WeaponEffect[] weaponEffects;
    public float timer;
    public void Init(){
        timer = 0f;
        //afterEffect.transform.localScale = new Vector3(afterEffectSize,afterEffectSize,1);
        //gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }
    void Update(){
        Move();
        LifeTimeCheck();
    }
    private void Move(){
        transform.position += transform.up * speed * Time.deltaTime;
    }
    protected virtual void LifeTimeCheck(){
        timer+=Time.deltaTime;
        if(timer>=lifeTime) Die();
    }
    protected virtual void OnHit(Idamageable character){
        character.TakeDamage(damage);
    }
    protected virtual void OnHit(Enemy character){
        bool died = character.TakeDamage(damage);
        if(player==null) return;
        foreach(var fx in weaponEffects) if(fx.does[0])fx.OnHit(player.weaponController,character);
        if(statusEffect!=null) statusEffect.Apply(character.effectsHandler);
        if(died) foreach(var fx in weaponEffects) if(fx.does[1])fx.OnKill(player.weaponController,character);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider){
        print(collider.name+" was hit");
        Idamageable character = collider.GetComponent<Idamageable>();
        Enemy enemy = collider.GetComponent<Enemy>();
        if(peirce<=0) Die();
        if(enemy!=null) OnHit(enemy);
        else{
            if(character!=null) OnHit(character);
            else Die();
        }
        peirce--;
    }
    private void Die(){
        if(afterEffect!=null) afterEffect.SetActive(true);
        Destroy(gameObject);
    }
}