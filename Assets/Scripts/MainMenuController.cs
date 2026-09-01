using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
class MainMenuController : MonoBehaviour{
    public GameObject menu;
    public GameObject profilesMenu;
    CustomButton[] menuButtons;
    void Start(){
        menu.SetActive(true);
        profilesMenu.SetActive(false);
        menuButtons = menu.GetComponentsInChildren<CustomButton>();
        menuButtons[0].onClick.AddListener(()=>OpenProfiles(false));
        menuButtons[1].onClick.AddListener(()=>OpenProfiles(true));
        menuButtons.Last().onClick.AddListener(()=>ExitGame());
    }
    private void OpenProfiles(bool manage){
        profilesMenu.SetActive(true);
        menu.SetActive(false);
        profilesMenu.GetComponent<profilesMenuController>().Init(manage);
    }
    private void ExitGame(){
        Application.Quit();
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            return;
        #endif
    }
}