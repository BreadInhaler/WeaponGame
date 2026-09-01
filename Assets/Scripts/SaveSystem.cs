using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
class SaveData{
    public PlayerSaveData playerProfile;
    public GlobalSaveData globalUnlocks;
}
[System.Serializable]
class GlobalSaveData{

}
[System.Serializable]
class PlayerSaveData{
    public byte id;
    public string name;
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
    public static void SaveGame(SaveData data, int pathNumber){
        Directory.CreateDirectory(Path.GetDirectoryName(savePath+pathNumber+".json"));
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath+pathNumber+".json",json);
        Debug.Log("Game saved to -> "+savePath+pathNumber+".json");
    }
    public static SaveData LoadGame(int pathNumber){
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
}