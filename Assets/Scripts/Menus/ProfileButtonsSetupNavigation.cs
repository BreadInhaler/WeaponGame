using System.Linq;
using UnityEngine;
class ProfileButtonsSetupNavigation : MonoBehaviour{
    public GameObject[] profiles;
    public CustomButton backButton;
    public byte maxPerRow = 4;

    private void Temp(){
        print("runned init");
        ProfilesMenuController menuController = GetComponent<ProfilesMenuController>();
        profiles = menuController.profiles;
        backButton.down = profiles[0].GetComponentsInChildren<CustomButton>()[0];
    }

    public void RefreshOne(byte changedIndex){
        RewireProfile(changedIndex);

        byte prevIndex = (byte)((changedIndex - 1 + profiles.Length) % profiles.Length);
        byte nextIndex = (byte)((changedIndex + 1) % profiles.Length);
        RewireProfile(prevIndex);
        RewireProfile(nextIndex);

        // Also re-check vertical neighbors (row above/below), since they point INTO this profile too
        if (changedIndex + maxPerRow < profiles.Length) RewireProfile((byte)(changedIndex + maxPerRow));
        if (changedIndex - maxPerRow >= 0) RewireProfile((byte)(changedIndex - maxPerRow));
    }

    void RewireProfile(byte i){
        CustomButton[] currentButtons = profiles[i].GetComponentsInChildren<CustomButton>(true);
        Debug.Log($"RewireProfile({i}) — profile: {profiles[i].name}, button count: {currentButtons.Length}");
        byte prevIndex = (byte)((i - 1 + profiles.Length) % profiles.Length);
        byte nextIndex = (byte)((i + 1) % profiles.Length);
        CustomButton[] prevButtons = profiles[prevIndex].GetComponentsInChildren<CustomButton>(true);
        CustomButton[] nextButtons = profiles[nextIndex].GetComponentsInChildren<CustomButton>(true);

        bool hasData = SaveSystem.LoadGameData((byte)(i+1)) != null;
        bool prevHasData = SaveSystem.LoadGameData((byte)(prevIndex+1)) != null;
        bool nextHasData = SaveSystem.LoadGameData((byte)(nextIndex+1)) != null;

        if(hasData){
            currentButtons[(byte)buttonsType.edit].right = currentButtons[(byte)buttonsType.delete];
            currentButtons[(byte)buttonsType.delete].left = currentButtons[(byte)buttonsType.edit];
            currentButtons[(byte)buttonsType.edit].left = prevHasData ? prevButtons[(byte)buttonsType.delete] : prevButtons[(byte)buttonsType.select];
            currentButtons[(byte)buttonsType.delete].right = nextHasData ? nextButtons[(byte)buttonsType.edit] : nextButtons[(byte)buttonsType.select];
        }else{
            currentButtons[(byte)buttonsType.select].left = prevHasData ? prevButtons[(byte)buttonsType.delete] : prevButtons[(byte)buttonsType.select];
            currentButtons[(byte)buttonsType.select].right = nextHasData ? nextButtons[(byte)buttonsType.edit] : nextButtons[(byte)buttonsType.select];
        }

        bool isTopRow = i < maxPerRow;
        bool hasRowBelow = i + maxPerRow < profiles.Length;
        bool hasRowAbove = i - maxPerRow >= 0;

        if(isTopRow){
            if(hasData){
                currentButtons[(byte)buttonsType.edit].up = backButton;
                currentButtons[(byte)buttonsType.delete].up = backButton;
            }else{
                currentButtons[(byte)buttonsType.select].up = backButton;
            }
            if(hasRowBelow){
                byte belowIndex = (byte)(i+maxPerRow);
                bool belowHasData = SaveSystem.LoadGameData((byte)(belowIndex+1)) != null;
                CustomButton[] belowButtons = profiles[belowIndex].GetComponentsInChildren<CustomButton>(true);
                if(hasData){
                    currentButtons[(byte)buttonsType.edit].down = belowHasData ? belowButtons[(byte)buttonsType.edit] : belowButtons[(byte)buttonsType.select];
                    currentButtons[(byte)buttonsType.delete].down = belowHasData ? belowButtons[(byte)buttonsType.delete] : belowButtons[(byte)buttonsType.select];
                }else{
                    currentButtons[(byte)buttonsType.select].down = belowHasData ? belowButtons[(byte)buttonsType.edit] : belowButtons[(byte)buttonsType.select];
                }
            }
        }else{
            if(hasData){
                currentButtons[(byte)buttonsType.edit].down = backButton;
                currentButtons[(byte)buttonsType.delete].down = backButton;
            }else{
                currentButtons[(byte)buttonsType.select].down = backButton;
            }
            if(hasRowAbove){
                byte aboveIndex = (byte)(i-maxPerRow);
                bool aboveHasData = SaveSystem.LoadGameData((byte)(aboveIndex+1)) != null;
                CustomButton[] aboveButtons = profiles[aboveIndex].GetComponentsInChildren<CustomButton>(true);
                if(hasData){
                    currentButtons[(int)buttonsType.edit].up = aboveHasData ? aboveButtons[(int)buttonsType.edit] : aboveButtons[(int)buttonsType.select];
                    currentButtons[(int)buttonsType.delete].up = aboveHasData ? aboveButtons[(int)buttonsType.delete] : aboveButtons[(int)buttonsType.select];
                }else{
                    currentButtons[(int)buttonsType.select].up = aboveHasData ? aboveButtons[(int)buttonsType.edit] : aboveButtons[(int)buttonsType.select];
                }
            }
        }
    }
    public void Init(){
        ProfilesMenuController menuController = GetComponent<ProfilesMenuController>();
        profiles = menuController.profiles;
        for(byte i=0;i<profiles.Length;i++){
            RewireProfile(i);
        }
        // backButton wiring stays the same, unchanged
        bool firstHasData = SaveSystem.LoadGameData(1) != null;
        CustomButton[] firstButtons = profiles[0].GetComponentsInChildren<CustomButton>(true);
        backButton.down = firstHasData ? firstButtons[(byte)buttonsType.edit] : firstButtons[(byte)buttonsType.select];
    }
}

enum buttonsType{
    edit,
    delete,
    select
}