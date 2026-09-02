using System.Linq;
using UnityEngine;

class profilesMenuController : MonoBehaviour{
    public GameObject[] profiles;
    public bool editMode=false;
    public void Init(bool manage){
        for(byte i=0;i<profiles.Count() ;i++){
            byte index = i;
            CustomButton[] buttons = profiles[i].GetComponentsInChildren<CustomButton>(true);
            print("amount of buttons -> "+buttons.Length);
            SaveData data = SaveSystem.LoadGameData(i);
            if(data==null){
                buttons[2].onClick.AddListener(()=>CreateNewProfile(index));
            }
            if(manage==true){
                editMode=true;
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(false);
            }else{
                buttons[0].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(true);
            }
        }
    }
    void Start(){
        CustomButton backButton = GetComponentInChildren<CustomButton>();
        backButton.onClick.AddListener(()=>Back());
    }
    void CreateNewProfile(byte id){
        SaveData data = new SaveData();
        data.playerProfile = new PlayerSaveData();
        data.playerProfile.id = (byte)(id+1);
        data.playerProfile.name = "Player "+(byte)(id+1);
        SaveSystem.SaveGameData(data,(byte)(id+1));
    }
    void Back(){
        FindAnyObjectByType<MainMenuController>().menu.SetActive(true);
        gameObject.SetActive(false);
    }
}