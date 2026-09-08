using System.Linq;
using TMPro;
using UnityEngine;

class ProfilesMenuController : MonoBehaviour{
    public GameObject[] profiles;
    public bool editMode=false;
    public GameObject conf;
    public void Init(bool manage){
        RefreshMenu(manage);
        gameObject.GetComponent<ProfileButtonsSetupNavigation>().Init();
    }
    void RefreshMenu(bool manage){
        editMode = manage;

        for (byte i = 0; i < profiles.Length; i++){
            byte index = (byte)(i + 1);
            CustomButton[] buttons = profiles[i].GetComponentsInChildren<CustomButton>(true);
            SaveData data = SaveSystem.LoadGameData(index);
            TextMeshPro text = profiles[i].GetComponentInChildren<TextMeshPro>();

            bool hasProfile = data != null;
            bool showManageButtons = manage && hasProfile;

            buttons[0].gameObject.SetActive(showManageButtons);
            buttons[1].gameObject.SetActive(showManageButtons);
            buttons[2].gameObject.SetActive(!showManageButtons);

            text.text = hasProfile ? data.playerProfile.name : "Empty";
            buttons[2].GetComponentInChildren<TextMeshPro>().text = hasProfile ? "Select" : "Create";

            buttons[1].onClick.RemoveAllListeners();
            buttons[1].onClick.AddListener(() => OpenConfiramation(index));

            buttons[2].onClick.RemoveAllListeners();
            buttons[2].onClick.AddListener(() => CreateNewProfile(index));
        }
    }
    void Start(){
        CustomButton backButton = GetComponentInChildren<CustomButton>();
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(()=>Back());
    }
    void CreateNewProfile(byte id){
        SaveData data = new SaveData();
        data.playerProfile.id = id;
        data.playerProfile.name = "Player "+id;
        data.playerProfile.melleWeapon = "scythe";
        data.playerProfile.rangedWeapon = "bow";
        data.playerProfile.melleProgressData.weaponID = data.playerProfile.melleWeapon;
        data.playerProfile.rangedProgressData.weaponID = data.playerProfile.rangedWeapon;
        data.playerProfile.melleProgressData.nodes.Add(data.playerProfile.melleWeapon+"_root"); 
        data.playerProfile.rangedProgressData.nodes.Add(data.playerProfile.rangedWeapon+"_root"); 
        SaveSystem.SaveGameData(data,id);
        RefreshMenu(editMode);
        GetComponent<ProfileButtonsSetupNavigation>().RefreshOne((byte)(id-1)); // profile array index, not save id
    }
    void OpenConfiramation(byte id){
        conf.SetActive(true);
        CustomButton[] buttons = conf.GetComponentsInChildren<CustomButton>();
        buttons[0].onClick.RemoveAllListeners();
        buttons[0].onClick.AddListener(()=>DeleteProfile(id));

        buttons[1].onClick.RemoveAllListeners();
        buttons[1].onClick.AddListener(()=>CloseConfirmation());
    }
    void CloseConfirmation(){
        conf.SetActive(false);
        gameObject.SetActive(true);
    }
    void DeleteProfile(byte id){
        SaveSystem.DeleteSaveData(id);
        CloseConfirmation();
        RefreshMenu(editMode);
        GetComponent<ProfileButtonsSetupNavigation>().RefreshOne((byte)(id-1)); // profile array index, not save id
    }
    void Back(){
        FindAnyObjectByType<MainMenuController>().menu.SetActive(true);
        gameObject.SetActive(false);
    }
}