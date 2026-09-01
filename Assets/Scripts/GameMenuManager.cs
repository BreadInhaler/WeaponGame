using UnityEngine;

class GameMenuManager : MonoBehaviour{
    public GameObject menuPanel;
    private CustomButton[] pauseButtons;
    bool menuOpen = false;
    Player player;
    MenuNavigator navigator;
    void Start(){
        menuPanel.SetActive(false);
        pauseButtons = menuPanel.GetComponentsInChildren<CustomButton>();

        pauseButtons[0].onClick.AddListener(()=>Resume()); 
        navigator = menuPanel.GetComponentInChildren<MenuNavigator>();
        navigator.buttons = pauseButtons;
        //ArrangeButtonsVertically();
    }
    void Resume(){
        ToggleMenu(player);
    }
    public void ToggleMenu(Player player){
        if(player!=this.player && menuOpen) return;
        menuOpen = !menuOpen;
        this.player = player;
        navigator.playerInput = player.playerInput;
        navigator.enabled=menuOpen;
        menuPanel.SetActive(menuOpen);
        GameManager.Instance.TogglePause();
    }
    void ArrangeButtonsVertically(){
        float totalHeight = (pauseButtons.Length - 1) * 1;
        float startY = totalHeight / 2f;

        for (int i = 0; i < pauseButtons.Length; i++){
            Vector3 pos = pauseButtons[i].transform.localPosition;
            pos.y = startY - (i * 1);
            pauseButtons[i].transform.localPosition = pos;
        }
    }
}