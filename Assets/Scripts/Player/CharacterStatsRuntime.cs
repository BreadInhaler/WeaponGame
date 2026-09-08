[System.Serializable]
public class CharacterStatsRuntime{
    public float currentHP;
    public float maxHP;
    public float damage;
    public float defense;
    public float speed;
    public CharacterStatsRuntime(){
        this.maxHP=0;
        currentHP=maxHP;
        this.speed=0;
        this.defense=0;
        this.damage=0;
    }
    public CharacterStatsRuntime(float maxHP,float speed,float defense,float damage,float jumpForce){
        this.maxHP=maxHP;
        currentHP=maxHP;
        this.speed=speed;
        this.defense=defense;
        this.damage=damage;
    }
    public CharacterStatsRuntime(CharacterStats stats){
        this.maxHP=stats.maxHP;
        currentHP=maxHP;
        this.speed=stats.speed;
        this.defense=stats.defense;
        this.damage=stats.damage;
    }
    public CharacterStatsRuntime(float hp,float maxHP,float defense,float damage,float speed,float jumpForce){
        this.currentHP=hp;
        this.maxHP=maxHP;
        this.speed=speed;
        this.defense=defense;
        this.damage=damage;
    }
    public CharacterStatsRuntime CloneStats(){
        return new CharacterStatsRuntime(
            currentHP,
            maxHP,
            defense,
            damage,
            speed
        );
    }
}
