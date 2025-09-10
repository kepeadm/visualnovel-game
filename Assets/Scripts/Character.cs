using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

[System.Serializable]
public class Character : MonoBehaviour
{
    public void setCharacter(string name){
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        playerData.nowCharacter = name;
        openCharacter();
    }
    public void Start(){
        openCharacter();
    }
    public void openCharacterAll(){
        GameObject.Find("Canvas").transform.Find("Character").gameObject.SetActive(true);
        GameObject characterObject = GameObject.Find("Canvas").transform.Find("Character").gameObject;
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        foreach(CharacterData characterData in playerData.characterData){
            if(characterData.name == playerData.nowCharacter){
                characterObject.transform.Find("hair").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(characterData.hair.spritePath);
                characterObject.transform.Find("hair").gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(0, 0);
                characterObject.transform.Find("hair").gameObject.GetComponent<Image>().SetNativeSize();
                characterObject.transform.Find("cloth").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(characterData.cloth.spritePath);
                characterObject.transform.Find("cloth").gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(0, 0);
                characterObject.transform.Find("cloth").gameObject.GetComponent<Image>().SetNativeSize();
                characterObject.transform.Find("body").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + characterData.name + "/" + characterData.name+ "_body");
                characterObject.transform.Find("body").gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(0, 0);
                characterObject.transform.Find("body").gameObject.GetComponent<Image>().SetNativeSize();
            }
        }
    }
    public void openCharacter(){
        GameObject characterObject = GameObject.Find("Canvas").transform.Find("Character").gameObject;
        PlayerData playerData = GameObject.Find("Gamemanager").GetComponent<Player>().playerData;
        foreach(CharacterData characterData in playerData.characterData){
            if(characterData.name == playerData.nowCharacter){
                characterObject.transform.Find("hair").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(characterData.hair.spritePath);
                characterObject.transform.Find("hair").gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(0, 0);
                characterObject.transform.Find("hair").gameObject.GetComponent<Image>().SetNativeSize();
                characterObject.transform.Find("cloth").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(characterData.cloth.spritePath);
                characterObject.transform.Find("cloth").gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(0, 0);
                characterObject.transform.Find("cloth").gameObject.GetComponent<Image>().SetNativeSize();
                characterObject.transform.Find("body").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + characterData.name + "/" + characterData.name+ "_body");
                characterObject.transform.Find("body").gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(0, 0);
                characterObject.transform.Find("body").gameObject.GetComponent<Image>().SetNativeSize();
            }
        }
    }
}