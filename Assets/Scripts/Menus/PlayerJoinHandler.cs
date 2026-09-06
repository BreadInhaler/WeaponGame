using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class JoinManager : MonoBehaviour{
    public static JoinManager Instance;
    public List<PlayerInput> joinedPlayers = new List<PlayerInput>();

    void Awake(){
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        PlayerInputManager.instance.onPlayerJoined += HandlePlayerJoined;
    }

    void OnDestroy(){
        if (PlayerInputManager.instance != null) PlayerInputManager.instance.onPlayerJoined -= HandlePlayerJoined;
    }

    void HandlePlayerJoined(PlayerInput newPlayer){
        joinedPlayers.Add(newPlayer);
        Debug.Log("Player joined: " + newPlayer.playerIndex + " using " + newPlayer.currentControlScheme);
    }
}