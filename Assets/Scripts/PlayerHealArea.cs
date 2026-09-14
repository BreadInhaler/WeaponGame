using System.Collections.Generic;
using UnityEngine;

public class PlayerHealArea : MonoBehaviour{
    public List<Player> players;
    public float timer=0;
    public float timerTick = 1;
    public float healAmount=10;
    void OnTriggerEnter2D(Collider2D collision){
        Player player = collision.GetComponent<Player>();
        if(player==null) return;
        players.Add(player);
    }
    void OnTriggerExit2D(Collider2D collision){
        Player player = collision.GetComponent<Player>();
        if(player==null) return;
        players.Remove(player);
    }
    void Update(){
        timer+=Time.deltaTime;
        if(timer>timerTick) foreach(Player player in players) player.RecieveHeal(healAmount);
    }
}