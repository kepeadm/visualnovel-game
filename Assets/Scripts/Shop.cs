using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Shop : MonoBehaviour
{
    public string shopname;
    public GameObject shopObject;
    public GameObject shopItemList;
    public GameObject shopPanel;
    public GameObject shopitemPrefab;
    public ShopData shopData = new ShopData();
    public void openShop_Main(){
        shopObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("main").gameObject.SetActive(false);
    }
    public void openShop_button(string shop){
        if(shop == "cloth"){
            string shop2;
            PlayerData playerData = this.gameObject.GetComponent<Player>().playerData;
            shop2 = playerData.nowCharacter + "_cloth";
            openShop_panel(shop2);
        }
        else if(shop == "hair"){
            string shop2;
            PlayerData playerData = this.gameObject.GetComponent<Player>().playerData;
            shop2 = playerData.nowCharacter + "_hair";
            openShop_panel(shop2);
        }
        else{
            openShop_panel(shop);
        }
    }
    public void openShop_panel(string shop){
        shopname = shop;
        shopData = loadShopJson(shop, shopData);
        foreach(Transform child in shopItemList.transform){
            Destroy(child.gameObject);
        }
        shopPanel.SetActive(true);
        foreach(ShopitemData itemData in shopData.itemData){
            GameObject shopitemIcon = Instantiate(shopitemPrefab, shopItemList.transform);
            shopitemIcon.name = itemData.name;
            shopitemIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>(itemData.spritePath);
            //shopitemIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            shopitemIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            shopitemIcon.GetComponent<Button>().onClick.AddListener(delegate{buyItem(shopitemIcon.name);});
            //shopitemIcon.GetComponent<Shopicon>().itemName = itemData.name;
            //shopitemIcon.GetComponent<Shopicon>().itemData = itemData;
            if(itemData.buyed == true){
                shopitemIcon.transform.Find("check").gameObject.SetActive(true);
            }
        }
    }
    public void buyItem(string itemName){
        foreach(ShopitemData itemData in shopData.itemData){
            if(itemData.name == itemName){
                if(itemData.buyed == true){
                    return;
                }
                else{
                    GameObject.Find("Canvas").transform.Find("confirm").gameObject.SetActive(true);
                    GameObject buyObject = GameObject.Find("Canvas").transform.Find("confirm").transform.Find("box").transform.Find("buy").gameObject;
                    GameObject textObject = GameObject.Find("Canvas").transform.Find("confirm").transform.Find("box").transform.Find("text").gameObject;
                    string buytext = "";
                    buytext = itemData.displayName + "을 " + itemData.price.amount.ToString() + "에 구매하시겠습니까?";
                    textObject.GetComponent<TextMeshProUGUI>().text = buytext;
                    buyObject.GetComponent<Buybutton>().itemName = itemName;
                    buyObject.GetComponent<Buybutton>().itemData = itemData;
                    return;
                }
            }
        }
        return;
    }
    public void buyListen(string itemName, ShopitemData itemData){
        PlayerData playerData = this.gameObject.GetComponent<Player>().playerData;
        GameObject.Find("Canvas").transform.Find("confirm").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("notice").gameObject.SetActive(true);
        if(playerData.gold >= itemData.price.amount){
            playerData.gold = playerData.gold - itemData.price.amount;
            GameObject textObject = GameObject.Find("Canvas").transform.Find("notice").transform.Find("box").transform.Find("text").gameObject;
            textObject.GetComponent<TextMeshProUGUI>().text="구매 완료하였습니다.";
            itemData.buyed = true;
            playerData.GetShopitemData(itemData);
            playerData.saveJson(playerData);
            foreach(Transform child in shopItemList.transform){
                if(child.name == itemName){
                    child.transform.Find("check").gameObject.SetActive(true);
                    break;
                }
            }
            return;
        }
        else{
            GameObject textObject = GameObject.Find("Canvas").transform.Find("notice").transform.Find("box").transform.Find("text").gameObject;
            textObject.GetComponent<TextMeshProUGUI>().text="금액이 부족합니다.";
            return;
        }
    }
    public void saveJson(ShopData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            path = Path.Combine(Application.persistentDataPath+ "ShopData" + ".json");
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer){
            path = Path.Combine(Application.persistentDataPath+ "ShopData" + ".json");
        }
        else{
            path = Path.Combine(Application.dataPath + "/local/Json/ShopData" + ".json");
        }
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }
    public ShopData loadShopJson(string shop, ShopData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/"+shop);
            data = JsonUtility.FromJson<ShopData>(textData.ToString());
            PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
            foreach(ShopitemData shopitemData in data.itemData){
                foreach(ItemData itemData in playerData.inventory){
                    if(shopitemData.name == itemData.name){
                        shopitemData.buyed = true;
                    }
                }
            }
            return data;
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/"+shop);
            data = JsonUtility.FromJson<ShopData>(textData.ToString());
            PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
            foreach(ShopitemData shopitemData in data.itemData){
                foreach(ItemData itemData in playerData.inventory){
                    if(shopitemData.name == itemData.name){
                        shopitemData.buyed = true;
                    }
                }
            }
            return data;
        }
        else{
            path = Path.Combine(Application.dataPath + "/Resources/Json/"+shop + ".json");
            if (System.IO.File.Exists(path)!=true){
                File.WriteAllText(path, "{}");
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<ShopData>(jsonData);
            PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
            foreach(ShopitemData shopitemData in data.itemData){
                foreach(ItemData itemData in playerData.inventory){
                    if(shopitemData.name == itemData.name){
                        shopitemData.buyed = true;
                    }
                }
            }
            return data;
        }
    }
    public ShopData loadJson(ShopData data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/Shop");
            data = JsonUtility.FromJson<ShopData>(textData.ToString());
            return data;
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/Shop");
            data = JsonUtility.FromJson<ShopData>(textData.ToString());
            return data;
        }
        else{
            path = Path.Combine(Application.dataPath + "/Resources/Json/Shop" + ".json");
            if (System.IO.File.Exists(path)!=true){
                File.WriteAllText(path, "{}");
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<ShopData>(jsonData);
            return data;
        }
    }
}

[System.Serializable]
public class ShopData{
    public string name;
    public List<ShopitemData> itemData = new List<ShopitemData>();
}

[System.Serializable]
public class ShopitemData{
    public string name;
    public string displayName;
    public string lore;
    public ShopMoney price = new ShopMoney();
    public string spritePath;
    public bool buyed = false;
}
[System.Serializable]
public class CharacterShop{
    public List<CharacterShopData> shop = new List<CharacterShopData>();
    public CharacterShop loadJson(CharacterShop data){
        string path;
        if(Application.platform == RuntimePlatform.Android){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/CharacterShop");
            data = JsonUtility.FromJson<CharacterShop>(textData.ToString());
            return data;
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer){
            TextAsset textData;
            textData = Resources.Load<TextAsset>("Json/CharacterShop");
            data = JsonUtility.FromJson<CharacterShop>(textData.ToString());
            return data;
        }
        else{
            path = Path.Combine(Application.dataPath + "/Resources/Json/CharacterShop" + ".json");
            if (System.IO.File.Exists(path)!=true){
                File.WriteAllText(path, "{}");
            }
            string jsonData = File.ReadAllText(path);
            data = JsonUtility.FromJson<CharacterShop>(jsonData);
            return data;
        }
    }
}

[System.Serializable]
public class CharacterShopData{
    public string name;
    public string displayName;
    public ShopMoney price = new ShopMoney();
}

[System.Serializable]
public class ShopMoney{
    public string type;
    public float amount;
}