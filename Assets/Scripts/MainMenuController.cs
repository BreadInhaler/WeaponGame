using System.Linq;
using UnityEditor;
using UnityEngine;
class MainMenuController : MonoBehaviour{
    GameObject menu;
    CustomButton[] menuButtons;
    void Start(){
        menu = gameObject;
        menuButtons = menu.GetComponentsInChildren<CustomButton>();

        menuButtons.Last().onClick.AddListener(()=>ExitGame());
    }
    private void ExitGame(){
        Application.Quit();
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            return;
        #endif
    }
}