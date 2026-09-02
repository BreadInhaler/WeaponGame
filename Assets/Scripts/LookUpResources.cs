using UnityEngine;
public static class LookUpResources{
    private static WeaponData[] allWeapons;
    private static MelleWeaponData[] allMelleWeapons;
    private static RangedWeaponData[] allRangedWeapons;
    private static string weaponPath = "Weapons";
    private static string mellePath = "/Melle";
    private static string rangedPath = "/Ranged";
    public static void Init(){
        allWeapons = Resources.LoadAll<WeaponData>(weaponPath);
        allMelleWeapons = Resources.LoadAll<MelleWeaponData>(weaponPath+mellePath);
        allRangedWeapons = Resources.LoadAll<RangedWeaponData>(weaponPath+rangedPath);
    }

    public static MelleWeaponData GetMelleWeaponByID(string id){
        for(byte i=0;i<allMelleWeapons.Length;i++) if(allMelleWeapons[i].id==id) return allMelleWeapons[i];
        Debug.Log("failed to find "+id+" in -> "+weaponPath+mellePath);
        return null;
    }
    public static RangedWeaponData GetRangedWeaponByID(string id){
        for(byte i=0;i<allRangedWeapons.Length;i++) if(allRangedWeapons[i].id==id) return allRangedWeapons[i];
        Debug.Log("failed to find "+id+" in -> "+weaponPath+rangedPath);
        return null;
    }
    public static WeaponData GetWeaponByID(string id){
        for(byte i=0;i<allWeapons.Length;i++) if(allWeapons[i].id==id) return allWeapons[i];
        Debug.Log("failed to find "+id+" in -> "+weaponPath);
        return null;
    }
}