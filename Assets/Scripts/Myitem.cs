using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Myitem : MonoBehaviour
{
    ShopData characterShopData = new ShopData();
    public GameObject characterObject;
    public GameObject myitemItemList;
    public GameObject myitemPanel;
    public GameObject myitemPrefab;
    public void Awake(){
    }
    public void openHair(){
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        foreach(Transform child in myitemItemList.transform){
            Destroy(child.gameObject);
        }
        foreach(ItemData itemData in playerData.inventory){
            if(itemData.name.Contains("hair") && itemData.name.Contains(playerData.nowCharacter)){
                GameObject myitemIcon = Instantiate(myitemPrefab, myitemItemList.transform);
                myitemIcon.name = itemData.name;
                myitemIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>(itemData.spritePath);
                myitemIcon.GetComponent<Button>().onClick.RemoveAllListeners();
                myitemIcon.GetComponent<Button>().onClick.AddListener(delegate{takeHair(myitemIcon.name);});
            }
        }
    }
    public void openCloth(){
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        foreach(Transform child in myitemItemList.transform){
            Destroy(child.gameObject);
        }
        foreach(ItemData itemData in playerData.inventory){
            if(itemData.name.Contains("cloth") && itemData.name.Contains(playerData.nowCharacter)){
                GameObject myitemIcon = Instantiate(myitemPrefab, myitemItemList.transform);
                myitemIcon.name = itemData.name;
                myitemIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>(itemData.spritePath);
                myitemIcon.GetComponent<Button>().onClick.RemoveAllListeners();
                myitemIcon.GetComponent<Button>().onClick.AddListener(delegate{takeCloth(myitemIcon.name);});
            }
        }
    }
    public void takeHair(string name){
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        foreach(CharacterData characterData in playerData.characterData){
            if(characterData.name == playerData.nowCharacter){
                characterData.hair = getItemDataInventory(name);
                characterObject.transform.Find("hair").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(characterData.hair.spritePath);
                characterObject.transform.Find("hair").gameObject.GetComponent<Image>().SetNativeSize();
                playerData.saveJson(playerData);
            }
        }
    }
    public void takeCloth(string name){
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        foreach(CharacterData characterData in playerData.characterData){
            if(characterData.name == playerData.nowCharacter){
                characterData.cloth = getItemDataInventory(name);
                characterObject.transform.Find("cloth").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(characterData.cloth.spritePath);
                characterObject.transform.Find("cloth").gameObject.GetComponent<Image>().SetNativeSize();
                playerData.saveJson(playerData);
            }
        }
    }
    public void openCharacter(){
        
    }
    public ItemData getItemDataInventory(string name){
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        foreach(ItemData itemData in playerData.inventory){
            if(itemData.name == name){
                return itemData;
            }
        }
        return null;
    }
}