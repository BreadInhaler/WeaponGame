using System.Collections.Generic;
using UnityEngine;
class GameManager : MonoBehaviour{
    public List<Player> players;
    private bool gamePaused = false;
    public static GameManager Instance{get; private set;}
    private CameraController cameraController;
    void Awake(){
        if(Instance != null && Instance !=this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Init();
        LookUpResources.Init();
        //DontDestroyOnLoad(gameObject);
    }
    void Init(){
        cameraController = Camera.main.GetComponent<CameraController>();
        foreach(GameObject obj in gameObject.scene.GetRootGameObjects()){
            Player player = obj.GetComponent<Player>();
            if(player!=null){
                print("arrived at fill player");
                players.Add(player);
                cameraController.players.Add(player.transform);
            }
        } 
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