using UnityEngine; 

class Projectile : MonoBehaviour{
    public Player player;
    public Sprite sprite;
    public GameObject afterEffect;
    public float lifeTime;
    public float peirce;
    public float speed;
    public float damage;
    public float homingStrenght;
    public float afterEffectSize;
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
    protected virtual void OnHitCharacter(Character character){
        character.TakeDamage(damage);
        player.RecieveHeal(player.weaponController.selectedWeapon.lifeSteal*damage);
    }
    protected virtual void OnTriggerEnter(Collider collider){
        Character character = collider.GetComponent<Character>();
        if(character!=null) OnHitCharacter(character);
        else Die();
        peirce--;
        if(peirce<=0) Die();
    }
    private void Die(){
        if(afterEffect!=null) afterEffect.SetActive(true);
        Destroy(gameObject);
    }
}