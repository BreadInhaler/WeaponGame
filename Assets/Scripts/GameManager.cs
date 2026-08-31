using System.Collections.Generic;
using UnityEngine;
class GameManager : MonoBehaviour{
    public List<Player> players;
    private bool gamePaused = false;
    public static GameManager Instance{get; private set;}
    void Awake(){
        if(Instance != null && Instance !=this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void TogglePause(){
        gamePaused=!gamePaused;
        foreach(Player player in players){
            if(gamePaused) player.menuInput.Disable();
            else player.menuInput.Enable();
        }
        if(gamePaused) Time.timeScale=0;
        else Time.timeScale=1;
    }
    public bool IsPaused(){
        return gamePaused;
    }
}