using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<UiData> uiObject = new List<UiData>();
    public bool hasCharacter(string name){
        PlayerData playerData = this.GetComponent<PlayerData>();
        foreach(CharacterData character in playerData.characterData){
            if(name == character.name){
                return true;
            }
        }
        return false;
    }
    public void Update(){
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        foreach(UiData uidata in uiObject){
            if(uidata.name == "gold"){
                if(uidata.obj.GetComponent<TextMeshProUGUI>().text != playerData.gold.ToString()){
                    uidata.obj.GetComponent<TextMeshProUGUI>().text = playerData.gold.ToString();
                }
            }
            else if(uidata.name == "progress"){
                if(uidata.obj.GetComponent<TextMeshProUGUI>().text != playerData.progress.ToString()){
                    uidata.obj.GetComponent<TextMeshProUGUI>().text = playerData.progress.ToString();
                }
            }
            else if(uidata.name == "power"){
                if(uidata.obj.GetComponent<TextMeshProUGUI>().text != playerData.power.ToString()){
                    uidata.obj.GetComponent<TextMeshProUGUI>().text = playerData.power.ToString();
                }
            }
        }
    }
}

[System.Serializable]
public class UiData{
    public string name;
    public GameObject obj;
}