using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
class SaveData{
    public PlayerSaveData playerProfile;
}
[System.Serializable]
class GlobalSaveData{
    public List<CosmeticWrapper> cosmetics = new List<CosmeticWrapper>();
    public GlobalSaveData(){
        for(byte i=0;i<10;i++) cosmetics.Add(new CosmeticWrapper(i+"",false));
    }
}
[System.Serializable]
class CosmeticWrapper{
    public string id;
    public bool unlocked;
    public CosmeticWrapper(string id, bool unlocked){
        this.id = id;
        this.unlocked = unlocked;
    }
}
[System.Serializable]
class PlayerSaveData{
    public byte id;
    public string name;
    public string melleWeapon="sword";
    public string rangedWeapon="bow";
}
[System.Serializable]
class WeaponProgressData{
    public string id;
    public List<SkillNodeData> nodes = new List<SkillNodeData>();
}
[System.Serializable]
class SkillNodeData{
    public string id;
    public byte upgradeCount;
}

static class SaveSystem{
    #if UNITY_EDITOR
        private static string absPath=Application.dataPath;
    #else
        private static string absPath=Application.persistentDataPath;
    #endif
    private static string savePath=absPath + "/Saves/saveFile";
    private static string globalUnlocksSavePath=absPath + "/Saves/globalUnlocks";
    public static void SaveGameData(SaveData data, byte pathNumber){
        Directory.CreateDirectory(Path.GetDirectoryName(savePath+pathNumber+".json"));
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath+pathNumber+".json",json);
        Debug.Log("Game saved to -> "+savePath+pathNumber+".json");
    }
    public static void SaveGlobalData(GlobalSaveData data){
        string json = JsonUtility.ToJson(data, true);
        Directory.CreateDirectory(Path.GetDirectoryName(globalUnlocksSavePath+".json"));
        File.WriteAllText(globalUnlocksSavePath+".json",json);
        Debug.Log("Game saved to -> "+globalUnlocksSavePath+".json");
    }
    public static SaveData LoadGameData(byte pathNumber){
        if(File.Exists(savePath+pathNumber+".json")){
           string json = File.ReadAllText(savePath+pathNumber+".json");
           SaveData data = JsonUtility.FromJson<SaveData>(json);
           Debug.Log("Game loaded from -> "+savePath+pathNumber+".json");
           return data;
        }else{
            Debug.LogWarning("Save file not found at: " + savePath+pathNumber+".json");
            return null;
        }
    }
    public static GlobalSaveData LoadGlobalData(){
        if(File.Exists(globalUnlocksSavePath+".json")){
           string json = File.ReadAllText(globalUnlocksSavePath+".json");
           GlobalSaveData data = JsonUtility.FromJson<GlobalSaveData>(json);
           Debug.Log("Game loaded from -> "+globalUnlocksSavePath+".json");
           return data;
        }else{
            Debug.LogWarning("Save file not found at: " + globalUnlocksSavePath+".json");
            return null;
        }
    }
}