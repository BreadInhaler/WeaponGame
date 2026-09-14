using UnityEngine;

public class HUDHandler : MonoBehaviour{
    public GameObject[] playerHuds;
    private SpriteRenderer[] fillBars = new SpriteRenderer[4];
    void Start(){
        for(byte i=0;i<GameManager.Instance.players.Count;i++) playerHuds[i].SetActive(true); //enable players health bars
        for(int i=GameManager.Instance.players.Count;i<playerHuds.Length;i++) playerHuds[i].SetActive(false); //disable unused player health bars
        for(byte i=0;i<playerHuds.Length;i++) fillBars[i]=playerHuds[i].GetComponentInChildren<SpriteRenderer>().GetComponentsInChildren<SpriteRenderer>()[1];
    }
    public void UpdateHealthBar(int id,float currHealth,float maxHealth){
        fillBars[id].size=new Vector2(currHealth/maxHealth,fillBars[id].size.y);
    }
}
