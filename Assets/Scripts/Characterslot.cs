using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

[System.Serializable]
public class Characterslot : MonoBehaviour
{
    public string charactername;
    public void Start(){
        if(this.gameObject.name == "locked"){
            PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
            foreach(CharacterData characterdata in playerData.characterData){
                if(characterdata.name == charactername){
                    Destroy(this.gameObject);
                }
            }
        }
    }
    public void buyCharacter(string name){
        CharacterShop cshop = new CharacterShop();
        cshop = cshop.loadJson(cshop);
        CharacterShopData cshopdata = new CharacterShopData();
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        foreach(CharacterData characterData in playerData.characterData){
            if(characterData.name == name){
                return;
            }
        }
        foreach(CharacterShopData characterShopData in cshop.shop){
            if(characterShopData.name == name){
                cshopdata = characterShopData;
            }
        }
        GameObject.Find("Canvas").transform.Find("characterconfirm").gameObject.SetActive(true);
        GameObject buyObject = GameObject.Find("Canvas").transform.Find("characterconfirm").transform.Find("box").transform.Find("buy").gameObject;
        GameObject textObject = GameObject.Find("Canvas").transform.Find("characterconfirm").transform.Find("box").transform.Find("text").gameObject;
        string buytext = "";
        buytext = "캐릭터 "+ cshopdata.displayName + " 을(를) " + cshopdata.price.amount.ToString() + " 에 구매하시겠습니까?";
        textObject.GetComponent<TextMeshProUGUI>().text = buytext;
        buyObject.GetComponent<Button>().onClick.RemoveAllListeners();
        buyObject.GetComponent<Button>().onClick.AddListener(delegate{buyListen(name,cshopdata);});
        return;
    }
    public void buyListen(string name, CharacterShopData cshopdata){
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        GameObject.Find("Canvas").transform.Find("characterconfirm").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("notice").gameObject.SetActive(true);
        if(playerData.gold >= cshopdata.price.amount){
            playerData.gold = playerData.gold - cshopdata.price.amount;
            GameObject textObject = GameObject.Find("Canvas").transform.Find("notice").transform.Find("box").transform.Find("text").gameObject;
            textObject.GetComponent<TextMeshProUGUI>().text="구매 완료하였습니다.";
            CharacterData characterData = new CharacterData();
            characterData.name = name;
            playerData.characterData.Add(characterData);
            playerData.saveJson(playerData);
            GameObject uicharacter = GameObject.Find("Ui_character").transform.Find(name).gameObject;
            foreach(Transform child in uicharacter.transform){
                if(child.name == "locked"){
                    Destroy(child.gameObject);
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
}