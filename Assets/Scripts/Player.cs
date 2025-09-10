using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Player : MonoBehaviour
{
    public PlayerData playerData;
    public void Awake(){
        playerData = playerData.loadJson(playerData);
    }

}

[System.Serializable]
public class PlayerData{
    public List<CharacterData> characterData = new List<CharacterData>();
    public string nowCharacter;
    public float progress;
    public float power;
    public float gold;
    public List<ItemData> inventory = new List<ItemData>();
    public List<SaveData> saveData = new List<SaveData>();
    public SaveData loadData = new SaveData();
    public void GetShopitemData(ShopitemData shopitemData){
        ItemData itemData = new ItemData();
        itemData.name = shopitemData.name;
        itemData.lore = shopitemData.lore;
        itemData.displayName = shopitemData.displayName;
        itemData.spritePath = shopitemData.spritePath;
        inventory.Add(itemData);
    }
    public CharacterData getCharacter(string name){
        foreach(CharacterData cD in characterData){
            if(cD.name == name){
                return cD;
            }
        }
        return null;
    }
    public void saveJson(PlayerData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            path = Path.Combine(Application.persistentDataPath+ "PlayerData" + ".json");
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer){
            path = Path.Combine(Application.persistentDataPath+ "PlayerData" + ".json");
        }
        else{
            path = Path.Combine(Application.dataPath + "/local/Json/PlayerData" + ".json");
        }
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }
    public PlayerData loadJson(PlayerData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            path = Path.Combine(Application.persistentDataPath+ "PlayerData" + ".json");
            if (System.IO.File.Exists(path)!=true){
                TextAsset textData;
                textData = Resources.Load<TextAsset>("Json/playerDefault");
                data = JsonUtility.FromJson<PlayerData>(textData.ToString());
                File.WriteAllText(path, textData.ToString());
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<PlayerData>(jsonData);
            return data;
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer){
            path = Path.Combine(Application.persistentDataPath+ "PlayerData" + ".json");
            if (System.IO.File.Exists(path)!=true){
                TextAsset textData;
                textData = Resources.Load<TextAsset>("Json/playerDefault");
                data = JsonUtility.FromJson<PlayerData>(textData.ToString());
                File.WriteAllText(path, textData.ToString());
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<PlayerData>(jsonData);
            return data;
        }
        else{
            path = Path.Combine(Application.dataPath + "/local/Json/PlayerData" + ".json");
            if (System.IO.File.Exists(path)!=true){
                TextAsset textData;
                textData = Resources.Load<TextAsset>("Json/playerDefault");
                data = JsonUtility.FromJson<PlayerData>(textData.ToString());
                File.WriteAllText(path, textData.ToString());
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<PlayerData>(jsonData);
            return data;
        }
    }
}

[System.Serializable]
public class CharacterData{
    public string name;
    public ItemData hair = new ItemData();
    public ItemData cloth = new ItemData();
    public int exp;
}

[System.Serializable]
public class ItemData{
    public string name;
    public string lore;
    public string displayName;
    public string spritePath;
    public Sprite getSprite(){
        return Resources.Load<Sprite>(spritePath);
    }
}

[System.Serializable]
public class SaveData{
    public string storyname;
    public int part;
    public int partindex;
    public int scriptindex;
}